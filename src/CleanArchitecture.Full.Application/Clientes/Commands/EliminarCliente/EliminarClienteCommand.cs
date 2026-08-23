using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Commands.EliminarCliente;

public record EliminarClienteCommand(Guid Id) : IRequest<bool>;
