namespace SGCAE.Application.AccessPoints.Commands;

public record CreateAccessPointCommand(string Name, string? Description, bool IsEnabled = true) : ICommand<Guid>;

public class CreateAccessPointHandler : ICommandHandler<CreateAccessPointCommand, Guid>
{
    private readonly IAccessPointRepository _repo;
    private readonly IUnitOfWork _uow;
    public CreateAccessPointHandler(IAccessPointRepository repo, IUnitOfWork uow) { _repo = repo; _uow = uow; }

    public async Task<Guid> Handle(CreateAccessPointCommand c, CancellationToken ct)
    {
        var point = AccessPoint.Create(c.Name, c.Description, c.IsEnabled);
        await _repo.Add(point, ct);
        await _uow.SaveChanges(ct);
        return point.Id;
    }
}