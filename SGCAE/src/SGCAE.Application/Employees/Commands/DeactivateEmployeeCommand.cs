namespace SGCAE.Application.Employees.Commands;

public record DeactivateEmployeeCommand(Guid Id) : ICommand<Unit>;

public class DeactivateEmployeeHandler : ICommandHandler<DeactivateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;
    public DeactivateEmployeeHandler(IEmployeeRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Unit> Handle(DeactivateEmployeeCommand c, CancellationToken ct)
    {
        var employee = await _repo.GetById(c.Id, ct)
            ?? throw new NotFoundException("El empleado no existe.");

        employee.Deactivate();
        await _uow.SaveChanges(ct);
        return Unit.Value;
    }
}