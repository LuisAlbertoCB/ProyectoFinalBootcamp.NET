namespace CleanArchitecture.Full.Domain;

public class Cuenta
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = "Corriente";
    public string Moneda { get; set; } = "USD";
    public decimal Saldo { get; set; }
    public decimal LimiteSobregiro { get; set; }
    public string Estado { get; set; } = "Activa";
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
}
