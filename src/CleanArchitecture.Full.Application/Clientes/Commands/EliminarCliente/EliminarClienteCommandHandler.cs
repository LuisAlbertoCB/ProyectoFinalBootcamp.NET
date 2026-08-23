using CleanArchitecture.Full.Domain;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Clientes.Commands.EliminarCliente;

public class EliminarClienteCommandHandler(
    IRepositorioCliente repositorioCliente,
    IRepositorioCuenta repositorioCuenta,
    ILogger<EliminarClienteCommandHandler> logger)
    : IRequestHandler<EliminarClienteCommand, bool>
{
    public async Task<bool> Handle(EliminarClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await repositorioCliente.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (cliente is null)
        {
            logger.LogWarning(
                "Se intentó eliminar el Cliente {ClienteId}, pero no existe",
                request.Id);
            return false;
        }

        var cuentas = await repositorioCuenta.ObtenerPorClienteIdAsync(request.Id, cancellationToken);
        if (cuentas.Count > 0)
        {
            logger.LogWarning(
                "No se pudo eliminar el Cliente {ClienteId} ({NumeroDocumento}) porque tiene {CantidadCuentas} cuenta(s) asociada(s)",
                cliente.Id,
                cliente.NumeroDocumento,
                cuentas.Count);

            throw new ValidationException(
            [
                new ValidationFailure(nameof(request.Id), "No se puede eliminar un cliente con cuentas asociadas.")
            ]);
        }

        repositorioCliente.Eliminar(cliente);
        await repositorioCliente.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "El Cliente {ClienteId} ({NumeroDocumento}) fue eliminado",
            cliente.Id,
            cliente.NumeroDocumento);

        return true;
    }
}
