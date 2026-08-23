using CleanArchitecture.Full.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.ActualizarCuenta;

public record ActualizarCuentaCommand(
    Guid Id,
    string TipoCuenta,
    string Moneda,
    decimal Saldo,
    decimal LimiteSobregiro,
    string Estado) : IRequest<CuentaDto?>;
