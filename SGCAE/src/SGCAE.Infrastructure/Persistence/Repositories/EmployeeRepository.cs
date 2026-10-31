namespace SGCAE.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _db;
    public EmployeeRepository(AppDbContext db) => _db = db;

    public Task<Employee?> GetById(Guid id, CancellationToken ct) =>
        _db.Employees.FirstOrDefaultAsync(e => e.Id == id, ct);

    public Task<bool> ExistsByIdentification(string identification, CancellationToken ct) =>
        _db.Employees.AnyAsync(e => e.IdentificationNumber == identification, ct);

    public async Task Add(Employee employee, CancellationToken ct) =>
        await _db.Employees.AddAsync(employee, ct);
}