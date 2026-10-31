namespace SGCAE.Application.Employees;

public record EmployeeDto(
    Guid Id, string IdentificationNumber, string FirstName, string LastName,
    string Email, string Position, string Area, string Status);