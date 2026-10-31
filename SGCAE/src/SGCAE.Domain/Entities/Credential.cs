using SGCAE.Domain.Common;
using SGCAE.Domain.Enums;

namespace SGCAE.Domain.Entities;

public class Credential
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = null!;   // código único
    public Guid EmployeeId { get; private set; }
    public CredentialStatus Status { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private Credential() { }

    public static Credential Assign(Guid employeeId, string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new DomainException("El código es obligatorio.");
        return new Credential
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            Code = code.Trim(),
            Status = CredentialStatus.Active,
            AssignedAt = DateTime.UtcNow
        };
    }

    public void Block()
    {
        if (Status == CredentialStatus.Revoked) throw new DomainException("No se puede bloquear una credencial revocada.");
        Status = CredentialStatus.Blocked;
    }

    public void Revoke() => Status = CredentialStatus.Revoked;

    public bool CanBeUsed => Status == CredentialStatus.Active;
}