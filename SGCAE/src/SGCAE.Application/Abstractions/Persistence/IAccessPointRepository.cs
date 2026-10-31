namespace SGCAE.Application.Abstractions.Persistence;

public interface IAccessPointRepository
{
    Task<AccessPoint?> GetById(Guid id, CancellationToken ct);
    Task Add(AccessPoint p, CancellationToken ct);
}