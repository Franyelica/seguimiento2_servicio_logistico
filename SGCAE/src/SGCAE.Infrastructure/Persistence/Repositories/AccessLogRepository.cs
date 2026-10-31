namespace SGCAE.Infrastructure.Persistence.Repositories;

public class AccessLogRepository : IAccessLogRepository
{
    private readonly AppDbContext _db;
    public AccessLogRepository(AppDbContext db) => _db = db;

    public async Task Add(AccessLog log, CancellationToken ct) =>
        await _db.AccessLogs.AddAsync(log, ct);
}