namespace SGCAE.Api.Requests;

public record UpdateEmployeeRequest(
    string FirstName, string LastName, string Email, string Position, string Area);