using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;

namespace CleanArchitecture.Full.Application.Clientes;

public static class ExtensionesMapeoCliente
{
    public static ClienteDto ToDto(this Cliente cliente) =>
        new(
            cliente.Id,
            cliente.Nombre,
            cliente.Apellido,
            cliente.NumeroDocumento,
            cliente.CorreoElectronico,
            cliente.Telefono,
            cliente.FechaNacimiento,
            cliente.Estado,
            cliente.FechaCreacion);
}
