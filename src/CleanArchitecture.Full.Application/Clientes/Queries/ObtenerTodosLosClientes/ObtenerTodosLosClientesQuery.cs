using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Queries.ObtenerTodosLosClientes;

public record ObtenerTodosLosClientesQuery : IRequest<IReadOnlyList<ClienteDto>>;
