namespace SGCAE.Application.Employees.Commands;

public record UpdateEmployeeCommand(
    Guid Id, string FirstName, string LastName,
    string Email, string Position, string Area) : ICommand<Unit>;

public class UpdateEmployeeHandler : ICommandHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;
    public UpdateEmployeeHandler(IEmployeeRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Unit> Handle(UpdateEmployeeCommand c, CancellationToken ct)
    {
        var employee = await _repo.GetById(c.Id, ct)
            ?? throw new NotFoundException("El empleado no existe.");

        employee.Update(c.FirstName, c.LastName, c.Email, c.Position, c.Area);
        await _uow.SaveChanges(ct);
        return Unit.Value;
    }
}