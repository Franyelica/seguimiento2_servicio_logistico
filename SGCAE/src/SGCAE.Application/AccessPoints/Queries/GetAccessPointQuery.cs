namespace SGCAE.Application.AccessPoints.Queries;

public record GetAccessPointsQuery(bool? OnlyEnabled) : IQuery<IReadOnlyList<AccessPointDto>>;

public class GetAccessPointsHandler : IQueryHandler<GetAccessPointsQuery, IReadOnlyList<AccessPointDto>>
{
    private readonly IAccessPointReadService _read;
    public GetAccessPointsHandler(IAccessPointReadService read) => _read = read;

    public Task<IReadOnlyList<AccessPointDto>> Handle(GetAccessPointsQuery q, CancellationToken ct) =>
        _read.GetAll(q.OnlyEnabled, ct);
}