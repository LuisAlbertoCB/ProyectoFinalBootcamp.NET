using FluentValidation;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.EliminarCuenta;

public class EliminarCuentaCommandValidator : AbstractValidator<EliminarCuentaCommand>
{
    public EliminarCuentaCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
