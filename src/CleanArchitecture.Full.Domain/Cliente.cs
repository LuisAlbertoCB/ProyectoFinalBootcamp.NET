namespace CleanArchitecture.Full.Domain;

public class Cliente
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public DateOnly FechaNacimiento { get; set; }
    public string Estado { get; set; } = "Activo";
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaActualizacion { get; set; }
    public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}
