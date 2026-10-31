using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.AccessPoints.Commands;
using SGCAE.Application.AccessPoints.Queries;
using SGCAE.Application.AccessPoints;
using SGCAE.Application.Access.Commands;
using SGCAE.Application.Access.Queries;
using SGCAE.Application.Access;
using SGCAE.Application.Credentials.Commands;
using SGCAE.Application.Credentials.Queries;
using SGCAE.Application.Credentials;
using SGCAE.Application.Employees.Commands;
using SGCAE.Application.Employees.Queries;
using SGCAE.Application.Employees;

namespace SGCAE.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection s)
    {
        // Employees
        s.AddScoped<ICommandHandler<CreateEmployeeCommand, Guid>, CreateEmployeeHandler>();
        s.AddScoped<ICommandHandler<UpdateEmployeeCommand, Unit>, UpdateEmployeeHandler>();
        s.AddScoped<ICommandHandler<DeactivateEmployeeCommand, Unit>, DeactivateEmployeeHandler>();
        s.AddScoped<IQueryHandler<GetEmployeeByIdQuery, EmployeeDto>, GetEmployeeByIdHandler>();
        s.AddScoped<IQueryHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>, GetEmployeesHandler>();

        // Credentials
        s.AddScoped<ICommandHandler<AssignCredentialCommand, Guid>, AssignCredentialHandler>();
        s.AddScoped<ICommandHandler<BlockCredentialCommand, Unit>, BlockCredentialHandler>();
        s.AddScoped<ICommandHandler<RevokeCredentialCommand, Unit>, RevokeCredentialHandler>();
        s.AddScoped<IQueryHandler<GetCredentialStatusQuery, CredentialDto>, GetCredentialStatusHandler>();

        // AccessPoints
        s.AddScoped<ICommandHandler<CreateAccessPointCommand, Guid>, CreateAccessPointHandler>();
        s.AddScoped<IQueryHandler<GetAccessPointsQuery, IReadOnlyList<AccessPointDto>>, GetAccessPointsHandler>();

        // Access
        s.AddScoped<ICommandHandler<RegisterAccessCommand, RegisterAccessResult>, RegisterAccessHandler>();
        s.AddScoped<IQueryHandler<GetAccessHistoryQuery, PagedResult<AccessLogDto>>, GetAccessHistoryHandler>();

        return s;
    }
}