using CleanArchitecture.Full.Domain;
using FluentValidation;

namespace CleanArchitecture.Full.Application.Clientes.Commands.ActualizarCliente;

public class ActualizarClienteCommandValidator : AbstractValidator<ActualizarClienteCommand>
{
    public ActualizarClienteCommandValidator(IRepositorioCliente repositorio)
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Apellido).NotEmpty().MaximumLength(80);
        RuleFor(x => x.NumeroDocumento).NotEmpty().MaximumLength(30)
            .MustAsync(async (command, numeroDocumento, cancellationToken) =>
            {
                var existente = await repositorio.ObtenerPorNumeroDocumentoAsync(numeroDocumento, cancellationToken);
                return existente is null || existente.Id == command.Id;
            })
            .WithMessage("Ya existe un cliente con el NumeroDocumento especificado.");
        RuleFor(x => x.CorreoElectronico).NotEmpty().MaximumLength(150).EmailAddress()
            .MustAsync(async (command, correoElectronico, cancellationToken) =>
            {
                var existente = await repositorio.ObtenerPorCorreoAsync(correoElectronico, cancellationToken);
                return existente is null || existente.Id == command.Id;
            })
            .WithMessage("Ya existe un cliente con el CorreoElectronico especificado.");
        RuleFor(x => x.Telefono).NotEmpty().MaximumLength(30);
        RuleFor(x => x.FechaNacimiento)
            .NotEmpty()
            .LessThan(x => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("FechaNacimiento debe ser una fecha pasada.");
        RuleFor(x => x.Estado).NotEmpty().Must(e => e is "Activo" or "Inactivo")
            .WithMessage("Estado debe ser 'Activo' o 'Inactivo'.");
    }
}
