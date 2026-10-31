namespace SGCAE.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChanges(CancellationToken ct);
}