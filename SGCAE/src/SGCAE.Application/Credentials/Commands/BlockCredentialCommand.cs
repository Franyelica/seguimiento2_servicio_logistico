namespace SGCAE.Application.Credentials.Commands;

public record BlockCredentialCommand(Guid CredentialId) : ICommand<Unit>;

public class BlockCredentialHandler : ICommandHandler<BlockCredentialCommand, Unit>
{
	private readonly ICredentialRepository _repo;
	private readonly IUnitOfWork _uow;
	public BlockCredentialHandler(ICredentialRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

	public async Task<Unit> Handle(BlockCredentialCommand c, CancellationToken ct)
	{
		var credential = await _repo.GetById(c.CredentialId, ct)
			?? throw new NotFoundException("La credencial no existe.");
		credential.Block();
		await _uow.SaveChanges(ct);
		return Unit.Value;
	}
}