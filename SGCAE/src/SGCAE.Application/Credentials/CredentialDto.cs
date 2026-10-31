namespace SGCAE.Application.Credentials;

public record CredentialDto(Guid Id, string Code, Guid EmployeeId, string Status, bool CanBeUsed, DateTime AssignedAt);