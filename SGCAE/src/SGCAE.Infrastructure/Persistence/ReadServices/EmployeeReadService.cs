using SGCAE.Application.Employees;

namespace SGCAE.Infrastructure.Persistence.ReadServices;

public class EmployeeReadService : IEmployeeReadService
{
    private readonly AppDbContext _db;
    public EmployeeReadService(AppDbContext db) => _db = db;

    public Task<EmployeeDto?> GetById(Guid id, CancellationToken ct) =>
        _db.Employees.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new EmployeeDto(x.Id, x.IdentificationNumber, x.FirstName, x.LastName,
                x.Email.Value, x.Position, x.Area, x.Status.ToString()))
            .FirstOrDefaultAsync(ct);

    public async Task<PagedResult<EmployeeDto>> Search(
        string? search, bool? onlyActive, int page, int pageSize, CancellationToken ct)
    {
        var query = _db.Employees.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(x => x.FirstName.Contains(term)
                                  || x.LastName.Contains(term)
                                  || x.IdentificationNumber.Contains(term));
        }

        if (onlyActive == true)
            query = query.Where(x => x.Status == EmploymentStatus.Active);

        var total = await query.CountAsync(ct);
        var items = await query
            .OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .Select(x => new EmployeeDto(x.Id, x.IdentificationNumber, x.FirstName, x.LastName,
                x.Email.Value, x.Position, x.Area, x.Status.ToString()))
            .ToListAsync(ct);

        return new PagedResult<EmployeeDto>(items, page, pageSize, total);
    }
}