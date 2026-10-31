namespace SGCAE.Application.Employees;

public interface IEmployeeReadService
{
    Task<EmployeeDto?> GetById(Guid id, CancellationToken ct);
    Task<PagedResult<EmployeeDto>> Search(string? search, bool? onlyActive, int page, int pageSize, CancellationToken ct);
}