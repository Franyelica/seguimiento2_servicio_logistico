namespace SGCAE.Application.Credentials;

public interface ICredentialReadService
{
    Task<CredentialDto?> GetById(Guid id, CancellationToken ct);
}