using SGCAE.Application.Access.Queries;
namespace SGCAE.Application.Access;

public interface IAccessLogReadService
{
    Task<PagedResult<AccessLogDto>> Search(GetAccessHistoryQuery query, CancellationToken ct);
}