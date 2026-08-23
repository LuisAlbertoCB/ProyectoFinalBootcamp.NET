using FluentValidation;

namespace CleanArchitecture.Full.Application.Cuentas.Queries.ObtenerCuentaPorId;

public class ObtenerCuentaPorIdQueryValidator : AbstractValidator<ObtenerCuentaPorIdQuery>
{
    public ObtenerCuentaPorIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
