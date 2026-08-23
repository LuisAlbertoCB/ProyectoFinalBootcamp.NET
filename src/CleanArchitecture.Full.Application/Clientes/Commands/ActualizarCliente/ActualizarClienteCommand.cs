using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Commands.ActualizarCliente;

public record ActualizarClienteCommand(
    Guid Id,
    string Nombre,
    string Apellido,
    string NumeroDocumento,
    string CorreoElectronico,
    string Telefono,
    DateOnly FechaNacimiento,
    string Estado) : IRequest<ClienteDto?>;
