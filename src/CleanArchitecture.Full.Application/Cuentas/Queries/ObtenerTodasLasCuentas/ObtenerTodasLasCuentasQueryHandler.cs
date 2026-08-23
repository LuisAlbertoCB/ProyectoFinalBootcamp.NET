using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerTodasLasCuentas;

public class ObtenerTodasLasCuentasQueryHandler(IRepositorioCuenta repositorio) : IRequestHandler<ObtenerTodasLasCuentasQuery, IReadOnlyList<CuentaDto>>
{
    public async Task<IReadOnlyList<CuentaDto>> Handle(ObtenerTodasLasCuentasQuery request, CancellationToken cancellationToken)
    {
        var cuentas = await repositorio.ObtenerTodasAsync(cancellationToken);
        return cuentas.Select(c => c.ToDto()).ToList();
    }
}
