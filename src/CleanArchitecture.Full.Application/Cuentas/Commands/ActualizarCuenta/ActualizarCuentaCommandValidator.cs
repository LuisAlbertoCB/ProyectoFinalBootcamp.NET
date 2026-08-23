using FluentValidation;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.ActualizarCuenta;

public class ActualizarCuentaCommandValidator : AbstractValidator<ActualizarCuentaCommand>
{
    public ActualizarCuentaCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.TipoCuenta).NotEmpty().Must(t => t is "Corriente" or "Ahorro")
            .WithMessage("TipoCuenta debe ser 'Corriente' o 'Ahorro'.");
        RuleFor(x => x.Moneda).NotEmpty().Length(3)
            .Must(m => m is "USD" or "EUR" or "ARS" or "BRL")
            .WithMessage("Moneda debe ser una de: USD, EUR, ARS, BRL.");
        RuleFor(x => x.Saldo).GreaterThanOrEqualTo(0);
        RuleFor(x => x.LimiteSobregiro).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Estado).NotEmpty().Must(e => e is "Activa" or "Inactiva" or "Bloqueada" or "Cerrada")
            .WithMessage("Estado debe ser 'Activa', 'Inactiva', 'Bloqueada' o 'Cerrada'.");
    }
}
