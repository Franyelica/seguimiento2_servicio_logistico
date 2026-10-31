namespace SGCAE.Infrastructure.Persistence.Configurations;

public class AccessPointConfiguration : IEntityTypeConfiguration<AccessPoint>
{
    public void Configure(EntityTypeBuilder<AccessPoint> b)
    {
        b.ToTable("AccessPoints");
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).HasMaxLength(100).IsRequired();
        b.HasIndex(x => x.Name).IsUnique();
        b.Property(x => x.Description).HasMaxLength(300);
        b.Property(x => x.IsEnabled).IsRequired();
    }
}