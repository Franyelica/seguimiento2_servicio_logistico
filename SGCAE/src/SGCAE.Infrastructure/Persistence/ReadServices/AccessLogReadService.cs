using SGCAE.Application.Access;
using SGCAE.Application.Access.Queries;

namespace SGCAE.Infrastructure.Persistence.ReadServices;

public class AccessLogReadService : IAccessLogReadService
{
    private readonly AppDbContext _db;
    public AccessLogReadService(AppDbContext db) => _db = db;

    public async Task<PagedResult<AccessLogDto>> Search(GetAccessHistoryQuery q, CancellationToken ct)
    {
        var query = _db.AccessLogs.AsNoTracking().AsQueryable();

        if (q.EmployeeId.HasValue) query = query.Where(x => x.EmployeeId == q.EmployeeId.Value);
        if (q.From.HasValue) query = query.Where(x => x.OccurredAt >= q.From.Value);
        if (q.To.HasValue) query = query.Where(x => x.OccurredAt <= q.To.Value);
        if (q.AccessPointId.HasValue) query = query.Where(x => x.AccessPointId == q.AccessPointId.Value);
        if (q.Result.HasValue) query = query.Where(x => x.Result == q.Result.Value);

        var total = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x => x.OccurredAt)
            .Skip((q.Page - 1) * q.PageSize)   // saltar las páginas anteriores
            .Take(q.PageSize)                  // traer solo una página
            .Select(x => new AccessLogDto(x.Id, x.EmployeeId, x.CredentialCode, x.AccessPointId,
                x.Movement.ToString(), x.Result.ToString(), x.RejectionReason, x.OccurredAt))
            .ToListAsync(ct);

        return new PagedResult<AccessLogDto>(items, q.Page, q.PageSize, total);
    }
}