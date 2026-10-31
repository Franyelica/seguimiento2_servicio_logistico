namespace SGCAE.Infrastructure.Persistence.Configurations;

public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
{
    public void Configure(EntityTypeBuilder<Credential> b)
    {
        b.ToTable("Credentials");
        b.HasKey(x => x.Id);

        b.Property(x => x.Code).HasMaxLength(50).IsRequired();
        b.HasIndex(x => x.Code).IsUnique();                   // código único

        b.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.AssignedAt).IsRequired();

        b.HasOne<Employee>()
         .WithMany()
         .HasForeignKey(x => x.EmployeeId)
         .OnDelete(DeleteBehavior.Restrict);

        b.Ignore(x => x.CanBeUsed);
    }
}