using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.CrearCuenta;

public class CrearCuentaCommandHandler(
    IRepositorioCuenta repositorioCuenta,
    IRepositorioCliente repositorioCliente,
    ILogger<CrearCuentaCommandHandler> logger)
    : IRequestHandler<CrearCuentaCommand, CuentaDto>
{
    private const decimal SaldoMinimoRecomendado = 100m;

    public async Task<CuentaDto> Handle(CrearCuentaCommand request, CancellationToken cancellationToken)
    {
        var cliente = await repositorioCliente.ObtenerPorIdAsync(request.ClienteId, cancellationToken);
        if (cliente is null)
        {
            logger.LogWarning(
                "No se pudo crear la Cuenta {NumeroCuenta} porque el Cliente {ClienteId} no existe",
                request.NumeroCuenta,
                request.ClienteId);

            throw new ValidationException(
            [
                new ValidationFailure(nameof(request.ClienteId), "El cliente especificado no existe.")
            ]);
        }

        var cuenta = new Cuenta
        {
            Id = Guid.NewGuid(),
            ClienteId = request.ClienteId,
            NumeroCuenta = request.NumeroCuenta,
            TipoCuenta = request.TipoCuenta,
            Moneda = request.Moneda,
            Saldo = request.Saldo,
            LimiteSobregiro = request.LimiteSobregiro,
            Estado = request.Estado,
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        await repositorioCuenta.AgregarAsync(cuenta, cancellationToken);
        await repositorioCuenta.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "La Cuenta {CuentaId} ({NumeroCuenta}) fue creada para el cliente {ClienteId}",
            cuenta.Id,
            cuenta.NumeroCuenta,
            cuenta.ClienteId);

        if (cuenta.Saldo < SaldoMinimoRecomendado)
        {
            logger.LogWarning(
                "La Cuenta {NumeroCuenta} fue creada con saldo {Saldo} por debajo del mínimo recomendado {SaldoMinimo}",
                cuenta.NumeroCuenta,
                cuenta.Saldo,
                SaldoMinimoRecomendado);
        }

        cuenta.Cliente = cliente;
        return cuenta.ToDto();
    }
}
