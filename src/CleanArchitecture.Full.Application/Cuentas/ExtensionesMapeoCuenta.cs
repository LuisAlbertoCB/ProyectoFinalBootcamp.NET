using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;

namespace CleanArchitecture.Full.Application.Cuentas;

public static class ExtensionesMapeoCuenta
{
    public static CuentaDto ToDto(this Cuenta cuenta) =>
        new(
            cuenta.Id,
            cuenta.ClienteId,
            cuenta.Cliente is null ? string.Empty : $"{cuenta.Cliente.Nombre} {cuenta.Cliente.Apellido}",
            cuenta.NumeroCuenta,
            cuenta.TipoCuenta,
            cuenta.Moneda,
            cuenta.Saldo,
            cuenta.LimiteSobregiro,
            cuenta.Estado,
            cuenta.FechaCreacion,
            cuenta.FechaActualizacion);
}
