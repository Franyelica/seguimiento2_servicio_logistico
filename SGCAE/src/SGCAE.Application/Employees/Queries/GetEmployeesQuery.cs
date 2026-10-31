namespace SGCAE.Application.Employees.Queries;

public record GetEmployeesQuery(string? Search, bool? OnlyActive, int Page = 1, int PageSize = 20)
    : IQuery<PagedResult<EmployeeDto>>;

public class GetEmployeesHandler : IQueryHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
{
    private readonly IEmployeeReadService _read;
    public GetEmployeesHandler(IEmployeeReadService read) => _read = read;

    public Task<PagedResult<EmployeeDto>> Handle(GetEmployeesQuery q, CancellationToken ct)
    {
        var page = Math.Max(1, q.Page);
        var size = Math.Clamp(q.PageSize, 1, 100);
        return _read.Search(q.Search, q.OnlyActive, page, size, ct);
    }
}