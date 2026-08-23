using CleanArchitecture.Full.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.EliminarCuenta;

public class EliminarCuentaCommandHandler(IRepositorioCuenta repositorio, ILogger<EliminarCuentaCommandHandler> logger)
    : IRequestHandler<EliminarCuentaCommand, bool>
{
    public async Task<bool> Handle(EliminarCuentaCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await repositorio.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (cuenta is null)
        {
            logger.LogWarning(
                "Se intentó eliminar la Cuenta {CuentaId}, pero no existe",
                request.Id);
            return false;
        }

        repositorio.Eliminar(cuenta);
        await repositorio.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "La Cuenta {CuentaId} ({NumeroCuenta}) fue eliminada",
            cuenta.Id,
            cuenta.NumeroCuenta);

        return true;
    }
}
