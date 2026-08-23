using FluentValidation;

namespace CleanArchitecture.Full.Application.Clientes.Commands.EliminarCliente;

public class EliminarClienteCommandValidator : AbstractValidator<EliminarClienteCommand>
{
    public EliminarClienteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
