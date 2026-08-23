using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentaPorId;

public record ObtenerCuentaPorIdQuery(Guid Id) : IRequest<CuentaDto?>;
