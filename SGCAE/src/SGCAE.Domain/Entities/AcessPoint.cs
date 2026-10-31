using SGCAE.Domain.Common;
using SGCAE.Domain.Enums;

namespace SGCAE.Domain.Entities;

public class AccessPoint
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;        // Recepción, Parqueadero, Bodega...
    public string? Description { get; private set; }
    public bool IsEnabled { get; private set; }

    private AccessPoint() { }
    public static AccessPoint Create(string name, string? description, bool isEnabled = true)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("El nombre es obligatorio.");
        return new AccessPoint { Id = Guid.NewGuid(), Name = name.Trim(), Description = description, IsEnabled = isEnabled };
    }
}