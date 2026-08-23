using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentaPorId;

public class ObtenerCuentaPorIdQueryHandler(IRepositorioCuenta repositorio) : IRequestHandler<ObtenerCuentaPorIdQuery, CuentaDto?>
{
    public async Task<CuentaDto?> Handle(ObtenerCuentaPorIdQuery request, CancellationToken cancellationToken)
    {
        var cuenta = await repositorio.ObtenerPorIdAsync(request.Id, cancellationToken);
        return cuenta?.ToDto();
    }
}
