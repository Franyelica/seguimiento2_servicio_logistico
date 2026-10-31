namespace SGCAE.Application.Access.Queries;

public record GetAccessHistoryQuery(
    Guid? EmployeeId, DateTime? From, DateTime? To,
    Guid? AccessPointId, AccessResult? Result,
    int Page = 1, int PageSize = 20) : IQuery<PagedResult<AccessLogDto>>;

public class GetAccessHistoryHandler : IQueryHandler<GetAccessHistoryQuery, PagedResult<AccessLogDto>>
{
    private readonly IAccessLogReadService _read;
    public GetAccessHistoryHandler(IAccessLogReadService read) => _read = read;

    public Task<PagedResult<AccessLogDto>> Handle(GetAccessHistoryQuery q, CancellationToken ct)
    {
        var safe = q with { Page = Math.Max(1, q.Page), PageSize = Math.Clamp(q.PageSize, 1, 100) };
        return _read.Search(safe, ct);
    }
}