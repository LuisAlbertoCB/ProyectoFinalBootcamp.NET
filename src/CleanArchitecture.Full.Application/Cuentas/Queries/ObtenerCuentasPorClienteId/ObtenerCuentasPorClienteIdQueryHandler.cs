using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentasPorClienteId;

public class ObtenerCuentasPorClienteIdQueryHandler(IRepositorioCuenta repositorio)
    : IRequestHandler<ObtenerCuentasPorClienteIdQuery, IReadOnlyList<CuentaDto>>
{
    public async Task<IReadOnlyList<CuentaDto>> Handle(ObtenerCuentasPorClienteIdQuery request, CancellationToken cancellationToken)
    {
        var cuentas = await repositorio.ObtenerPorClienteIdAsync(request.ClienteId, cancellationToken);
        return cuentas.Select(c => c.ToDto()).ToList();
    }
}
