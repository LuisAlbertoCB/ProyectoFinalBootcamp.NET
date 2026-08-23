namespace CleanArchitecture.Full.Api.Endpoints.Dtos;

public record ActualizarCuentaBody(string TipoCuenta, string Moneda, decimal Saldo, decimal LimiteSobregiro, string Estado);
