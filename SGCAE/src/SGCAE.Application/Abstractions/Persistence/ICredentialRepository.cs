namespace SGCAE.Application.Abstractions.Persistence;

public interface ICredentialRepository
{
    Task<Credential?> GetById(Guid id, CancellationToken ct);
    Task<Credential?> GetByCode(string code, CancellationToken ct);
    Task Add(Credential c, CancellationToken ct);
}