namespace SGCAE.Application.Employees.Commands;

public record CreateEmployeeCommand(
    string IdentificationNumber, string FirstName, string LastName,
    string Email, string Position, string Area) : ICommand<Guid>;

public class CreateEmployeeHandler : ICommandHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository _repo;
    private readonly IUnitOfWork _uow;
    public CreateEmployeeHandler(IEmployeeRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Guid> Handle(CreateEmployeeCommand c, CancellationToken ct)
    {
        if (await _repo.ExistsByIdentification(c.IdentificationNumber.Trim(), ct))
            throw new DomainException("Ya existe un empleado con esa identificación.");

        var employee = Employee.Create(c.IdentificationNumber, c.FirstName, c.LastName,
                                       c.Email, c.Position, c.Area);
        await _repo.Add(employee, ct);
        await _uow.SaveChanges(ct);
        return employee.Id;
    }
}