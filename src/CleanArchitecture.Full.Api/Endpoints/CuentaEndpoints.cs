using CleanArchitecture.Full.Api.Endpoints.Dtos;
using CleanArchitecture.Full.Application.Cuentas.Commands.ActualizarCuenta;
using CleanArchitecture.Full.Application.Cuentas.Commands.CrearCuenta;
using CleanArchitecture.Full.Application.Cuentas.Commands.EliminarCuenta;
using CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentaPorId;
using CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerTodasLasCuentas;
using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Api.Endpoints;

public static class CuentaEndpoints
{
    public static void MapCuentaEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/cuentas").WithTags("Cuentas");

        group.MapGet("", async (ISender sender, CancellationToken cancellationToken) =>
            Results.Ok(await sender.Send(new ObtenerTodasLasCuentasQuery(), cancellationToken)))
            .Produces<IReadOnlyList<CuentaDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var cuenta = await sender.Send(new ObtenerCuentaPorIdQuery(id), cancellationToken);
            return cuenta is null ? Results.NotFound() : Results.Ok(cuenta);
        })
            .Produces<CuentaDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPost("", async (CrearCuentaCommand command, ISender sender, CancellationToken cancellationToken) =>
        {
            var cuenta = await sender.Send(command, cancellationToken);
            return Results.Created($"api/cuentas/{cuenta.Id}", cuenta);
        })
            .Produces<CuentaDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapPut("{id:guid}", async (Guid id, ActualizarCuentaBody body, ISender sender, CancellationToken cancellationToken) =>
        {
            var cuenta = await sender.Send(
                new ActualizarCuentaCommand(id, body.TipoCuenta, body.Moneda, body.Saldo, body.LimiteSobregiro, body.Estado),
                cancellationToken);
            return cuenta is null ? Results.NotFound() : Results.Ok(cuenta);
        })
            .Produces<CuentaDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        group.MapDelete("{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var eliminada = await sender.Send(new EliminarCuentaCommand(id), cancellationToken);
            return eliminada ? Results.NoContent() : Results.NotFound();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}
