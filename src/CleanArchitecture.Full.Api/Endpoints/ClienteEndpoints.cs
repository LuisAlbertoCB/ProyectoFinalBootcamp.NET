using CleanArchitecture.Full.Api.Endpoints.Dtos;
using CleanArchitecture.Full.Application.Clientes.Commands.ActualizarCliente;
using CleanArchitecture.Full.Application.Clientes.Commands.CrearCliente;
using CleanArchitecture.Full.Application.Clientes.Commands.EliminarCliente;
using CleanArchitecture.Full.Application.Clientes.Queries.ObtenerClientePorId;
using CleanArchitecture.Full.Application.Clientes.Queries.ObtenerTodosLosClientes;
using CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentasPorClienteId;
using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Api.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/clientes").WithTags("Clientes");

        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
            Results.Ok(await sender.Send(new ObtenerTodosLosClientesQuery(), cancellationToken)))
            .Produces<IReadOnlyList<ClienteDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var cliente = await sender.Send(new ObtenerClientePorIdQuery(id), cancellationToken);
            return cliente is null ? Results.NotFound() : Results.Ok(cliente);
        })
            .Produces<ClienteDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:guid}/cuentas", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            Results.Ok(await sender.Send(new ObtenerCuentasPorClienteIdQuery(id), cancellationToken)))
            .Produces<IReadOnlyList<CuentaDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async (CrearClienteCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var cliente = await sender.Send(command, cancellationToken);
            return Results.Created($"api/clientes/{cliente.Id}", cliente);
        })
            .Produces<ClienteDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:guid}", async (Guid id, ActualizarClienteBody body, ISender sender, CancellationToken cancellationToken) =>
        {
            var cliente = await sender.Send(
                new ActualizarClienteCommand(id, body.Nombre, body.Apellido, body.NumeroDocumento, body.CorreoElectronico, body.Telefono, body.FechaNacimiento, body.Estado),
                cancellationToken);
            return cliente is null ? Results.NotFound() : Results.Ok(cliente);
        })
            .Produces<ClienteDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var eliminado = await sender.Send(new EliminarClienteCommand(id), cancellationToken);
            return eliminado ? Results.NoContent() : Results.NotFound();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
