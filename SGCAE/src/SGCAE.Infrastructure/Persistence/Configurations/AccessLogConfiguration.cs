namespace SGCAE.Infrastructure.Persistence.Configurations;

public class AccessLogConfiguration : IEntityTypeConfiguration<AccessLog>
{
    public void Configure(EntityTypeBuilder<AccessLog> b)
    {
        b.ToTable("AccessLogs");
        b.HasKey(x => x.Id);

        b.Property(x => x.CredentialCode).HasMaxLength(50).IsRequired();
        b.Property(x => x.Movement).HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.Result).HasConversion<string>().HasMaxLength(20).IsRequired();
        b.Property(x => x.RejectionReason).HasMaxLength(200);
        b.Property(x => x.OccurredAt).IsRequired();

        // EmployeeId es nullable: un rechazo por credencial inexistente no tiene empleado
        b.HasOne<Employee>()
         .WithMany()
         .HasForeignKey(x => x.EmployeeId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasOne<AccessPoint>()
         .WithMany()
         .HasForeignKey(x => x.AccessPointId)
         .OnDelete(DeleteBehavior.Restrict);

        // Índices por las columnas que usa el RF-04 para filtrar
        b.HasIndex(x => x.OccurredAt);
        b.HasIndex(x => x.EmployeeId);
        b.HasIndex(x => x.AccessPointId);
    }
}