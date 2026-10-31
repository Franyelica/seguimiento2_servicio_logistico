namespace SGCAE.Application.AccessPoints;

public interface IAccessPointReadService
{
    Task<IReadOnlyList<AccessPointDto>> GetAll(bool? onlyEnabled, CancellationToken ct);
}