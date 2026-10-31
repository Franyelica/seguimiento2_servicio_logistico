namespace SGCAE.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> b)
    {
        b.ToTable("Employees");
        b.HasKey(x => x.Id);

        b.Property(x => x.IdentificationNumber).HasMaxLength(30).IsRequired();
        b.HasIndex(x => x.IdentificationNumber).IsUnique();   // identificación única

        b.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        b.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        b.Property(x => x.Position).HasMaxLength(100).IsRequired();
        b.Property(x => x.Area).HasMaxLength(100).IsRequired();

        // El objeto de valor Email se guarda como una columna de texto
        b.Property(x => x.Email)
            .HasConversion(e => e.Value, v => new Email(v))
            .HasColumnName("Email")
            .HasMaxLength(200)
            .IsRequired();

        // El enum se guarda como texto legible ("Active") y no como número
        b.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

        b.Ignore(x => x.IsActive);   // propiedad calculada, no es columna
    }
}