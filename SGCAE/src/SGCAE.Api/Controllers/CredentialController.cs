using Microsoft.AspNetCore.Mvc;
using SGCAE.Application.Abstractions;
using SGCAE.Application.Credentials;
using SGCAE.Application.Credentials.Commands;
using SGCAE.Application.Credentials.Queries;

namespace SGCAE.Api.Controllers;

[ApiController]
[Route("api/credentials")]
public class CredentialsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Assign(
        AssignCredentialCommand command,
        [FromServices] ICommandHandler<AssignCredentialCommand, Guid> handler,
        CancellationToken ct)
    {
        var id = await handler.Handle(command, ct);
        return CreatedAtAction(nameof(GetStatus), new { id }, new { id });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CredentialDto>> GetStatus(
        Guid id,
        [FromServices] IQueryHandler<GetCredentialStatusQuery, CredentialDto> handler,
        CancellationToken ct) =>
        Ok(await handler.Handle(new GetCredentialStatusQuery(id), ct));

    [HttpPatch("{id:guid}/block")]
    public async Task<IActionResult> Block(
        Guid id,
        [FromServices] ICommandHandler<BlockCredentialCommand, Unit> handler,
        CancellationToken ct)
    {
        await handler.Handle(new BlockCredentialCommand(id), ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}/revoke")]
    public async Task<IActionResult> Revoke(
        Guid id,
        [FromServices] ICommandHandler<RevokeCredentialCommand, Unit> handler,
        CancellationToken ct)
    {
        await handler.Handle(new RevokeCredentialCommand(id), ct);
        return NoContent();
    }
}