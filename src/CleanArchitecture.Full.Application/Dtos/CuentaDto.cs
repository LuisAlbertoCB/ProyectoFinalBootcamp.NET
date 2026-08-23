namespace CleanArchitecture.Full.Application.Dtos;

public record CuentaDto(
    Guid Id,
    Guid ClienteId,
    string NombreCompletoCliente,
    string NumeroCuenta,
    string TipoCuenta,
    string Moneda,
    decimal Saldo,
    decimal LimiteSobregiro,
    string Estado,
    DateTime FechaCreacion,
    DateTime FechaActualizacion);
