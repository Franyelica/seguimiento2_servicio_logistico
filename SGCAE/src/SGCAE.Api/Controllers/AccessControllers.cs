using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Abstractions;
using SGCAE.Application.Access;
using SGCAE.Application.Access.Commands;
using SGCAE.Application.Access.Queries;
using SGCAE.Domain.Enums;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/access")]
public class AccessController : ControllerBase
{
    // RF-03: registrar una entrada o salida (autorizada o rechazada)
    [HttpPost]
    public async Task<ActionResult<RegisterAccessResult>> Register(
        RegisterAccessCommand command,
        [FromServices] ICommandHandler<RegisterAccessCommand, RegisterAccessResult> handler,
        CancellationToken ct) =>
        Ok(await handler.Handle(command, ct));

    // RF-04: historial con filtros y paginación
    [HttpGet("history")]
    public async Task<ActionResult<PagedResult<AccessLogDto>>> GetHistory(
        [FromServices] IQueryHandler<GetAccessHistoryQuery, PagedResult<AccessLogDto>> handler,
        CancellationToken ct,
        [FromQuery] Guid? employeeId = null,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null,
        [FromQuery] Guid? accessPointId = null,
        [FromQuery] AccessResult? result = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20) =>
        Ok(await handler.Handle(
            new GetAccessHistoryQuery(employeeId, from, to, accessPointId, result, page, pageSize), ct));
}