using Microsoft.AspNetCore.Mvc;
using SGCAE.Api.Requests;
using SGCAE.Application.Abstractions;
using SGCAE.Application.Employees;
using SGCAE.Application.Employees.Commands;
using SGCAE.Application.Employees.Queries;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateEmployeeCommand command,
        [FromServices] ICommandHandler<CreateEmployeeCommand, Guid> handler,
        CancellationToken ct)
    {
        var id = await handler.Handle(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(
        Guid id,
        [FromServices] IQueryHandler<GetEmployeeByIdQuery, EmployeeDto> handler,
        CancellationToken ct) =>
        Ok(await handler.Handle(new GetEmployeeByIdQuery(id), ct));

    [HttpGet]
    public async Task<ActionResult<PagedResult<EmployeeDto>>> GetAll(
        [FromServices] IQueryHandler<GetEmployeesQuery, PagedResult<EmployeeDto>> handler,
        CancellationToken ct,
        [FromQuery] string? search = null,
        [FromQuery] bool? onlyActive = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20) =>
        Ok(await handler.Handle(new GetEmployeesQuery(search, onlyActive, page, pageSize), ct));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateEmployeeRequest request,
        [FromServices] ICommandHandler<UpdateEmployeeCommand, Unit> handler,
        CancellationToken ct)
    {
        await handler.Handle(new UpdateEmployeeCommand(
            id, request.FirstName, request.LastName, request.Email, request.Position, request.Area), ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        [FromServices] ICommandHandler<DeactivateEmployeeCommand, Unit> handler,
        CancellationToken ct)
    {
        await handler.Handle(new DeactivateEmployeeCommand(id), ct);
        return NoContent();
    }
}