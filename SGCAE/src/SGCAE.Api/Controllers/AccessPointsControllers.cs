using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.AccessPoints;
using SGCAE.Application.AccessPoints.Commands;
using SGCAE.Application.AccessPoints.Queries;
using SGCAE.Application.Abstractions;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/access-points")]
public class AccessPointsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateAccessPointCommand command,
        [FromServices] ICommandHandler<CreateAccessPointCommand, Guid> handler,
        CancellationToken ct)
    {
        var id = await handler.Handle(command, ct);
        return StatusCode(StatusCodes.Status201Created, new { id });
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AccessPointDto>>> GetAll(
        [FromServices] IQueryHandler<GetAccessPointsQuery, IReadOnlyList<AccessPointDto>> handler,
        CancellationToken ct,
        [FromQuery] bool? onlyEnabled = null) =>
        Ok(await handler.Handle(new GetAccessPointsQuery(onlyEnabled), ct));
}