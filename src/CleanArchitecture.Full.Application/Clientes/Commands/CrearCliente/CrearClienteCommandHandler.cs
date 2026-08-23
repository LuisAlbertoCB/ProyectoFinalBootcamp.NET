using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Full.Application.Clientes.Commands.CrearCliente;

public class CrearClienteCommandHandler(IRepositorioCliente repositorio, ILogger<CrearClienteCommandHandler> logger)
    : IRequestHandler<CrearClienteCommand, ClienteDto>
{
    public async Task<ClienteDto> Handle(CrearClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            NumeroDocumento = request.NumeroDocumento,
            CorreoElectronico = request.CorreoElectronico,
            Telefono = request.Telefono,
            FechaNacimiento = request.FechaNacimiento,
            Estado = "Activo",
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        await repositorio.AgregarAsync(cliente, cancellationToken);
        await repositorio.GuardarCambiosAsync(cancellationToken);

        logger.LogInformation(
            "El Cliente {ClienteId} ({NumeroDocumento}) fue creado",
            cliente.Id,
            cliente.NumeroDocumento);

        return cliente.ToDto();
    }
}
