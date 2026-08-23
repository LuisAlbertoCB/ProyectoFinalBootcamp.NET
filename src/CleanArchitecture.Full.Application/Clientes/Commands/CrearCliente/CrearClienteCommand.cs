using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Commands.CrearCliente;

public record CrearClienteCommand(
    string Nombre,
    string Apellido,
    string NumeroDocumento,
    string CorreoElectronico,
    string Telefono,
    DateOnly FechaNacimiento) : IRequest<ClienteDto>;
