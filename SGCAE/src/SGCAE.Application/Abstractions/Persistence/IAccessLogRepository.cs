namespace SGCAE.Application.Abstractions.Persistence;

public interface IAccessLogRepository
{
    Task Add(AccessLog log, CancellationToken ct);
}