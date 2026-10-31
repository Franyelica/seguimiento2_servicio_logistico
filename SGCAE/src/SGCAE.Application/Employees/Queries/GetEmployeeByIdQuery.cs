namespace SGCAE.Application.Employees.Queries;

public record GetEmployeeByIdQuery(Guid Id) : IQuery<EmployeeDto>;

public class GetEmployeeByIdHandler : IQueryHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    private readonly IEmployeeReadService _read;
    public GetEmployeeByIdHandler(IEmployeeReadService read) => _read = read;

    public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery q, CancellationToken ct) =>
        await _read.GetById(q.Id, ct) ?? throw new NotFoundException("El empleado no existe.");
}