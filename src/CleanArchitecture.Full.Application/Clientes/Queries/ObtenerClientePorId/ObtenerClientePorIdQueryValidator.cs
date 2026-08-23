using FluentValidation;

namespace CleanArchitecture.Full.Application.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdQueryValidator : AbstractValidator<ObtenerClientePorIdQuery>
{
    public ObtenerClientePorIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
