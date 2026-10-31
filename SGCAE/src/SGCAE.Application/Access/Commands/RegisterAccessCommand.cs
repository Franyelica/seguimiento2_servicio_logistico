namespace SGCAE.Application.Access.Commands;

public record RegisterAccessCommand(string CredentialCode, Guid AccessPointId, MovementType Movement)
    : ICommand<RegisterAccessResult>;

public record RegisterAccessResult(AccessResult Result, string? Reason, Guid LogId);

public class RegisterAccessHandler : ICommandHandler<RegisterAccessCommand, RegisterAccessResult>
{
    private readonly ICredentialRepository _credentials;
    private readonly IEmployeeRepository _employees;
    private readonly IAccessPointRepository _points;
    private readonly IAccessLogRepository _logs;
    private readonly IUnitOfWork _uow;

    public RegisterAccessHandler(ICredentialRepository c, IEmployeeRepository e,
        IAccessPointRepository p, IAccessLogRepository l, IUnitOfWork u)
    { _credentials = c; _employees = e; _points = p; _logs = l; _uow = u; }

    public async Task<RegisterAccessResult> Handle(RegisterAccessCommand cmd, CancellationToken ct)
    {
        var point = await _points.GetById(cmd.AccessPointId, ct)
            ?? throw new NotFoundException("El punto de acceso no existe.");

        var code = cmd.CredentialCode.Trim();
        var credential = await _credentials.GetByCode(code, ct);
        Employee? employee = credential is null
            ? null
            : await _employees.GetById(credential.EmployeeId, ct);

        string? reason = null;
        if (credential is null) reason = "Credencial inexistente.";
        else if (!credential.CanBeUsed) reason = $"Credencial {credential.Status}.";
        else if (employee is null || !employee.IsActive) reason = "Empleado inactivo.";
        else if (!point.IsEnabled) reason = "Punto de acceso deshabilitado.";

        var log = reason is null
            ? AccessLog.Authorized(employee!.Id, code, point.Id, cmd.Movement)
            : AccessLog.Rejected(employee?.Id, code, point.Id, cmd.Movement, reason);

        await _logs.Add(log, ct);
        await _uow.SaveChanges(ct);   // se guarda siempre, también los rechazados

        return new RegisterAccessResult(log.Result, log.RejectionReason, log.Id);
    }
}