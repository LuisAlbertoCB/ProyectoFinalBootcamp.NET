using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Queries.ObtenerClientePorId;

public record ObtenerClientePorIdQuery(Guid Id) : IRequest<ClienteDto?>;
