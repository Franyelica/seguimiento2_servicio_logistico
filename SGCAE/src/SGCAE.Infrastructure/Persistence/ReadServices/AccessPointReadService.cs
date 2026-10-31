using SGCAE.Application.AccessPoints;

namespace SGCAE.Infrastructure.Persistence.ReadServices;

public class AccessPointReadService : IAccessPointReadService
{
    private readonly AppDbContext _db;
    public AccessPointReadService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<AccessPointDto>> GetAll(bool? onlyEnabled, CancellationToken ct)
    {
        var query = _db.AccessPoints.AsNoTracking().AsQueryable();

        if (onlyEnabled == true)
            query = query.Where(x => x.IsEnabled);

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new AccessPointDto(x.Id, x.Name, x.Description, x.IsEnabled))
            .ToListAsync(ct);
    }
}