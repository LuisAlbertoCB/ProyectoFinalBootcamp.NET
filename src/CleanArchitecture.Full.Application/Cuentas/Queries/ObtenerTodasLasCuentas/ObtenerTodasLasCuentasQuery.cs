using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerTodasLasCuentas;

public record ObtenerTodasLasCuentasQuery : IRequest<IReadOnlyList<CuentaDto>>;
