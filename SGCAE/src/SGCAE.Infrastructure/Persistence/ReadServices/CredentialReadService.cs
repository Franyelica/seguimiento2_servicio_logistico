using SGCAE.Application.Credentials;

namespace SGCAE.Infrastructure.Persistence.ReadServices;

public class CredentialReadService : ICredentialReadService
{
    private readonly AppDbContext _db;
    public CredentialReadService(AppDbContext db) => _db = db;

    public Task<CredentialDto?> GetById(Guid id, CancellationToken ct) =>
        _db.Credentials.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new CredentialDto(x.Id, x.Code, x.EmployeeId, x.Status.ToString(),
                x.Status == CredentialStatus.Active, x.AssignedAt))
            .FirstOrDefaultAsync(ct);
}