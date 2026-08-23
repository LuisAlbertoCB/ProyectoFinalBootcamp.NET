using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Clientes.Commands.ActualizarCliente;

public class ActualizarClienteCommandHandler(IRepositorioCliente repositorio, ILogger<ActualizarClienteCommandHandler> logger)
    : IRequestHandler<ActualizarClienteCommand, ClienteDto?>
{
    public async Task<ClienteDto?> Handle(ActualizarClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await repositorio.ObtenerPorIdAsync(request.Id, cancellationToken);
        if (cliente is null)
        {
            logger.LogWarning(
                "Se intentó actualizar el Cliente {ClienteId}, pero no existe",
                request.Id);
            return null;
        }

        cliente.Nombre = request.Nombre;
        cliente.Apellido = request.Apellido;
        cliente.NumeroDocumento = request.NumeroDocumento;
        cliente.CorreoElectronico = request.CorreoElectronico;
        cliente.Telefono = request.Telefono;
        cliente.FechaNacimiento = request.FechaNacimiento;
        cliente.Estado = request.Estado;
        cliente.FechaActualizacion = DateTime.UtcNow;

        repositorio.Actualizar(cliente);
        await repositorio.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "El Cliente {ClienteId} ({NumeroDocumento}) fue actualizado",
            cliente.Id,
            cliente.NumeroDocumento);

        return cliente.ToDto();
    }
}
