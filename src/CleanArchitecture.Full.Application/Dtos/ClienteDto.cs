namespace CleanArchitecture.Full.Application.Dtos;

public record ClienteDto(
    Guid Id,
    string Nombre,
    string Apellido,
    string NumeroDocumento,
    string CorreoElectronico,
    string Telefono,
    DateOnly FechaNacimiento,
    string Estado,
    DateTime FechaCreacion);
