using SGCAE.Domain.Common;
using SGCAE.Domain.Enums;
using SGCAE.Domain.ValueObjects;
namespace SGCAE.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; }
    public string IdentificationNumber { get; private set; } = null!; // identificacion unica
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string Position { get; private set; } = null!;   // cargo
    public string Area { get; private set; } = null!;       // area organizacional
    public EmploymentStatus Status { get; private set; }

    private Employee() { }

    public static Employee Create(string identification, string firstName, string lastName,
                                  string email, string position, string area)
    {
        if (string.IsNullOrWhiteSpace(identification)) throw new DomainException("La identificación es obligatoria.");
        if (string.IsNullOrWhiteSpace(firstName)) throw new DomainException("Los nombres son obligatorios.");
        if (string.IsNullOrWhiteSpace(lastName)) throw new DomainException("Los apellidos son obligatorios.");
        return new Employee
        {
            Id = Guid.NewGuid(),
            IdentificationNumber = identification.Trim(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = new Email(email),
            Position = position,
            Area = area,
            Status = EmploymentStatus.Active
        };
    }

    public void Update(string firstName, string lastName, string email, string position, string area)
    {
        if (Status == EmploymentStatus.Inactive)
            throw new DomainException("No se puede actualizar un empleado inactivo.");
        FirstName = firstName; LastName = lastName;
        Email = new Email(email); Position = position; Area = area;
    }

    public void Deactivate()
    {
        if (Status == EmploymentStatus.Inactive) throw new DomainException("El empleado ya está inactivo.");
        Status = EmploymentStatus.Inactive;
    }

    public bool IsActive => Status == EmploymentStatus.Active;
}