namespace SGCAE.Application.Credentials.Commands;

public record AssignCredentialCommand(Guid EmployeeId, string Code) : ICommand<Guid>;

public class AssignCredentialHandler : ICommandHandler<AssignCredentialCommand, Guid>
{
    private readonly IEmployeeRepository _employees;
    private readonly ICredentialRepository _credentials;
    private readonly IUnitOfWork _uow;
    public AssignCredentialHandler(IEmployeeRepository e, ICredentialRepository c, IUnitOfWork u)
    { _employees = e; _credentials = c; _uow = u; }

    public async Task<Guid> Handle(AssignCredentialCommand c, CancellationToken ct)
    {
        var employee = await _employees.GetById(c.EmployeeId, ct)
            ?? throw new NotFoundException("El empleado no existe.");
        if (!employee.IsActive)
            throw new DomainException("No se puede asignar una credencial a un empleado inactivo.");
        if (await _credentials.GetByCode(c.Code.Trim(), ct) is not null)
            throw new DomainException("Ya existe una credencial con ese código.");

        var credential = Credential.Assign(employee.Id, c.Code);
        await _credentials.Add(credential, ct);
        await _uow.SaveChanges(ct);
        return credential.Id;
    }
}