using SGCAE.Domain.Enums;

namespace SGCAE.Domain.Entities;

public class AccessLog
{
    public Guid Id { get; private set; }
    public Guid? EmployeeId { get; private set; }   // null si la credencial no existe
    public string CredentialCode { get; private set; } = null!;
    public Guid AccessPointId { get; private set; }
    public MovementType Movement { get; private set; }
    public AccessResult Result { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime OccurredAt { get; private set; }

    private AccessLog() { }

    public static AccessLog Authorized(Guid employeeId, string code, Guid pointId, MovementType m) =>
        new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            CredentialCode = code,
            AccessPointId = pointId,
            Movement = m,
            Result = AccessResult.Authorized,
            OccurredAt = DateTime.UtcNow
        };

    public static AccessLog Rejected(Guid? employeeId, string code, Guid pointId, MovementType m, string reason) =>
        new()
        {
            Id = Guid.NewGuid(),
            EmployeeId = employeeId,
            CredentialCode = code,
            AccessPointId = pointId,
            Movement = m,
            Result = AccessResult.Rejected,
            RejectionReason = reason,
            OccurredAt = DateTime.UtcNow
        };
}