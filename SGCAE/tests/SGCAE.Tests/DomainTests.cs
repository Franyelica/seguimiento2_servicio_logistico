using SGCAE.Domain.Common;
using SGCAE.Domain.Entities;
using SGCAE.Domain.Enums;

namespace SGCAE.Tests;

public class DomainTests
{
    [Fact]
    public void Employee_Create_StartsActive()
    {
        var e = Employee.Create("123", "Ana", "Gómez", "ana@empresa.com", "Operaria", "Logística");
        Assert.True(e.IsActive);
    }

    [Fact]
    public void Employee_Create_WithInvalidEmail_Throws()
    {
        Assert.Throws<DomainException>(() =>
            Employee.Create("123", "Ana", "Gómez", "correo-invalido", "Operaria", "Logística"));
    }

    [Fact]
    public void Employee_Deactivate_Twice_Throws()
    {
        var e = Employee.Create("123", "Ana", "Gómez", "ana@empresa.com", "Operaria", "Logística");
        e.Deactivate();
        Assert.Throws<DomainException>(() => e.Deactivate());
    }

    [Fact]
    public void Credential_Block_WhenRevoked_Throws()
    {
        var c = Credential.Assign(Guid.NewGuid(), "CRED-1");
        c.Revoke();
        Assert.Throws<DomainException>(() => c.Block());
    }

    [Fact]
    public void Credential_Blocked_CannotBeUsed()
    {
        var c = Credential.Assign(Guid.NewGuid(), "CRED-1");
        c.Block();
        Assert.False(c.CanBeUsed);
        Assert.Equal(CredentialStatus.Blocked, c.Status);
    }
}