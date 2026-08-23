using CleanArchitecture.Full.Application.Clientes.Queries.ObtenerClientePorId;
using Xunit;

namespace CleanArchitecture.Full.Application.Tests.Clientes;

public class ObtenerClientePorIdQueryValidatorTests
{
    private readonly ObtenerClientePorIdQueryValidator _validator = new();

    [Fact]
    public void Id_vacio_no_debe_ser_valido()
    {
        var result = _validator.Validate(new ObtenerClientePorIdQuery(Guid.Empty));

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Id_valido_debe_pasar_la_validacion()
    {
        var result = _validator.Validate(new ObtenerClientePorIdQuery(Guid.NewGuid()));

        Assert.True(result.IsValid);
    }
}
