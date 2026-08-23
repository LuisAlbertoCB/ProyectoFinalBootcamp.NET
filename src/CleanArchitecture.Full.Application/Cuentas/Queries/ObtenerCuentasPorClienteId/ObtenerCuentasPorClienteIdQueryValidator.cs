using FluentValidation;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentasPorClienteId;

public class ObtenerCuentasPorClienteIdQueryValidator : AbstractValidator<ObtenerCuentasPorClienteIdQuery>
{
    public ObtenerCuentasPorClienteIdQueryValidator()
    {
        RuleFor(x => x.ClienteId).NotEmpty();
    }
}
