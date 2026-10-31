using SGCAE.Domain.Common;
namespace SGCAE.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }
    private Email() { Value = null!; }
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            throw new DomainException("El correo electrónico no es válido.");
        Value = value.Trim().ToLowerInvariant();
    }
}