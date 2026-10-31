namespace SGCAE.Infrastructure.Persistence;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Credential> Credentials => Set<Credential>();
    public DbSet<AccessPoint> AccessPoints => Set<AccessPoint>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Busca todas las clases IEntityTypeConfiguration<T> de este proyecto
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    // Implementa IUnitOfWork (el contrato definido en Application)
    public Task<int> SaveChanges(CancellationToken ct) => SaveChangesAsync(ct);
}