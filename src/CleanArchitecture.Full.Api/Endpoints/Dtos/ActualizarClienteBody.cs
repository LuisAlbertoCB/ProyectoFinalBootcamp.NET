namespace CleanArchitecture.Full.Api.Endpoints.Dtos;

public record ActualizarClienteBody(
    string Nombre,
    string Apellido,
    string NumeroDocumento,
    string CorreoElectronico,
    string Telefono,
    DateOnly FechaNacimiento,
    string Estado);
