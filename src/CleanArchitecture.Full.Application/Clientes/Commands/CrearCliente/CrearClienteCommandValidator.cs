using CleanArchitecture.Full.Domain;
using FluentValidation;

namespace CleanArchitecture.Full.Application.Clientes.Commands.CrearCliente;

public class CrearClienteCommandValidator : AbstractValidator<CrearClienteCommand>
{
    public CrearClienteCommandValidator(IRepositorioCliente repositorio)
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Apellido).NotEmpty().MaximumLength(80);
        RuleFor(x => x.NumeroDocumento).NotEmpty().MaximumLength(30)
            .MustAsync(async (numeroDocumento, cancellationToken) =>
                await repositorio.ObtenerPorNumeroDocumentoAsync(numeroDocumento, cancellationToken) is null)
            .WithMessage("Ya existe un cliente con el NumeroDocumento especificado.");
        RuleFor(x => x.CorreoElectronico).NotEmpty().MaximumLength(150).EmailAddress()
            .MustAsync(async (correoElectronico, cancellationToken) =>
                await repositorio.ObtenerPorCorreoAsync(correoElectronico, cancellationToken) is null)
            .WithMessage("Ya existe un cliente con el CorreoElectronico especificado.");
        RuleFor(x => x.Telefono).NotEmpty().MaximumLength(30);
        RuleFor(x => x.FechaNacimiento)
            .NotEmpty()
            .LessThan(x => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("FechaNacimiento debe ser una fecha pasada.");
    }
}
