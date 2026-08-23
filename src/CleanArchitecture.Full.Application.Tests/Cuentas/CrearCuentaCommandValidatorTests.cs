using CleanArchitecture.Full.Application.Cuentas.Commands.CrearCuenta;
using CleanArchitecture.Full.Domain;
using Xunit;

namespace CleanArchitecture.Full.Application.Tests.Cuentas;

public class CrearCuentaCommandValidatorTests
{
    private readonly CrearCuentaCommandValidator _validator = new(new RepositorioCuentaFalso());

    [Theory]
    [InlineData("Corriente")]
    [InlineData("Ahorro")]
    public async Task TipoCuenta_valido_debe_pasar_la_validacion(string tipoCuenta)
    {
        var comando = ComandoValido() with { TipoCuenta = tipoCuenta };

        var result = await _validator.ValidateAsync(comando);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task TipoCuenta_invalido_no_debe_ser_valido()
    {
        var comando = ComandoValido() with { TipoCuenta = "Empresarial" };

        var result = await _validator.ValidateAsync(comando);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Saldo_negativo_no_debe_ser_valido()
    {
        var comando = ComandoValido() with { Saldo = 1 };

        var result = await _validator.ValidateAsync(comando);

        Assert.False(result.IsValid);
    }

    private static CrearCuentaCommand ComandoValido() => new(
        ClienteId: Guid.NewGuid(),
        NumeroCuenta: "CTA-0000000001",
        TipoCuenta: "Corriente",
        Moneda: "USD",
        Saldo: 100,
        LimiteSobregiro: 0,
        Estado: "Activa");

    private sealed class RepositorioCuentaFalso : IRepositorioCuenta
    {
        public Task<IReadOnlyList<Cuenta>> ObtenerTodasAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Cuenta>>([]);

        public Task<Cuenta?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Task.FromResult<Cuenta?>(null);

        public Task<IReadOnlyList<Cuenta>> ObtenerPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Cuenta>>([]);

        public Task<Cuenta?> ObtenerPorNumeroCuentaAsync(string numeroCuenta, CancellationToken cancellationToken = default)
            => Task.FromResult<Cuenta?>(null);

        public Task AgregarAsync(Cuenta cuenta, CancellationToken cancellationToken = default)
            => Task.CompletedTask;

        public void Actualizar(Cuenta cuenta) { }

        public void Eliminar(Cuenta cuenta) { }

        public Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(true);
    }
}
