namespace SGCAE.Application.Credentials.Commands;

public record RevokeCredentialCommand(Guid CredentialId) : ICommand<Unit>;

public class RevokeCredentialHandler : ICommandHandler<RevokeCredentialCommand, Unit>
{
    private readonly ICredentialRepository _repo;
    private readonly IUnitOfWork _uow;
    public RevokeCredentialHandler(ICredentialRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Unit> Handle(RevokeCredentialCommand c, CancellationToken ct)
    {
        var credential = await _repo.GetById(c.CredentialId, ct)
            ?? throw new NotFoundException("La credencial no existe.");
        credential.Revoke();
        await _uow.SaveChanges(ct);
        return Unit.Value;
    }
}