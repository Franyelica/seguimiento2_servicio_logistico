namespace SGCAE.Application.Credentials.Queries;

public record GetCredentialStatusQuery(Guid CredentialId) : IQuery<CredentialDto>;

public class GetCredentialStatusHandler : IQueryHandler<GetCredentialStatusQuery, CredentialDto>
{
    private readonly ICredentialReadService _read;
    public GetCredentialStatusHandler(ICredentialReadService read) => _read = read;

    public async Task<CredentialDto> Handle(GetCredentialStatusQuery q, CancellationToken ct) =>
        await _read.GetById(q.CredentialId, ct) ?? throw new NotFoundException("La credencial no existe.");
}