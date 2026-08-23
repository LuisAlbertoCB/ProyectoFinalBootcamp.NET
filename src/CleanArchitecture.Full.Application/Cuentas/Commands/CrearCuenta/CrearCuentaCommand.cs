using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.CrearCuenta;

public record CrearCuentaCommand(
    Guid ClienteId,
    string NumeroCuenta,
    string TipoCuenta,
    string Moneda,
    decimal Saldo,
    decimal LimiteSobregiro,
    string Estado) : IRequest<CuentaDto>;
