namespace SGCAE.Application.Access;

public record AccessLogDto(
    Guid Id, Guid? EmployeeId, string CredentialCode, Guid AccessPointId,
    string Movement, string Result, string? RejectionReason, DateTime OccurredAt);