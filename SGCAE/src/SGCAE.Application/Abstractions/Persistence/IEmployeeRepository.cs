namespace SGCAE.Application.Abstractions.Persistence;

public interface IEmployeeRepository
{
    Task<Employee?> GetById(Guid id, CancellationToken ct);
    Task<bool> ExistsByIdentification(string identification, CancellationToken ct);
    Task Add(Employee e, CancellationToken ct);
}