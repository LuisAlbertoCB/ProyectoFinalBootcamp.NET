using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentasPorClienteId;

public record ObtenerCuentasPorClienteIdQuery(Guid ClienteId) : IRequest<IReadOnlyList<CuentaDto>>;
