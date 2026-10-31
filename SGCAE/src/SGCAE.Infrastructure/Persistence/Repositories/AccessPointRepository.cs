namespace SGCAE.Infrastructure.Persistence.Repositories;

public class AccessPointRepository : IAccessPointRepository
{
    private readonly AppDbContext _db;
    public AccessPointRepository(AppDbContext db) => _db = db;

    public Task<AccessPoint?> GetById(Guid id, CancellationToken ct) =>
        _db.AccessPoints.FirstOrDefaultAsync(p => p.Id == id, ct);

    public async Task Add(AccessPoint accessPoint, CancellationToken ct) =>
        await _db.AccessPoints.AddAsync(accessPoint, ct);
}