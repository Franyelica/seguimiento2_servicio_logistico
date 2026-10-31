namespace SGCAE.Application.AccessPoints;

public record AccessPointDto(Guid Id, string Name, string? Description, bool IsEnabled);