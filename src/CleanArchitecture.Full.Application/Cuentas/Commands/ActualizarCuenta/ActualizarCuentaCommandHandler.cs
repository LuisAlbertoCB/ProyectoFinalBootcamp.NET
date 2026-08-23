using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.ActualizarCuenta;

public class ActualizarCuentaCommandHandler(IRepositorioCuenta repositorio, ILogger<ActualizarCuentaCommandHandler> logger)
    : IRequestHandler<ActualizarCuentaCommand, CuentaDto?>
{
    private const decimal SaldoMinimoRecomendado = 100m;

    public async Task<CuentaDto?> Handle(ActualizarCuentaCommand request, CancellationToken cancellationToken)
    {
        var cuenta = await repositorio.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (cuenta is null)
        {
            logger.LogWarning(
                "Se intentó actualizar la Cuenta {CuentaId}, pero no existe",
                request.Id);
            return null;
        }

        cuenta.TipoCuenta = request.TipoCuenta;
        cuenta.Moneda = request.Moneda;
        cuenta.Saldo = request.Saldo;
        cuenta.LimiteSobregiro = request.LimiteSobregiro;
        cuenta.Estado = request.Estado;
        cuenta.FechaActualizacion = DateTime.UtcNow;

        repositorio.Actualizar(cuenta);
        await repositorio.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "La Cuenta {CuentaId} ({NumeroCuenta}) fue actualizada",
            cuenta.Id,
            cuenta.NumeroCuenta);

        if (cuenta.Saldo < SaldoMinimoRecomendado)
        {
            logger.LogWarning(
                "La Cuenta {NumeroCuenta} fue actualizada con saldo {Saldo} por debajo del mínimo recomendado {SaldoMinimo}",
                cuenta.NumeroCuenta,
                cuenta.Saldo,
                SaldoMinimoRecomendado);
        }

        return cuenta.ToDto();
    }
}
