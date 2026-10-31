namespace SGCAE.Infrastructure.Persistence.Repositories;

public class CredentialRepository : ICredentialRepository
{
    private readonly AppDbContext _db;
    public CredentialRepository(AppDbContext db) => _db = db;

    public Task<Credential?> GetById(Guid id, CancellationToken ct) =>
        _db.Credentials.FirstOrDefaultAsync(c => c.Id == id, ct);

    public Task<Credential?> GetByCode(string code, CancellationToken ct) =>
        _db.Credentials.FirstOrDefaultAsync(c => c.Code == code, ct);

    public async Task Add(Credential credential, CancellationToken ct) =>
        await _db.Credentials.AddAsync(credential, ct);
}