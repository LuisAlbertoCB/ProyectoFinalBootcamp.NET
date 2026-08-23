namespace CleanArchitecture.Full.Domain;

public interface IRepositorioCuenta
{
    Task<IReadOnlyList<Cuenta>> ObtenerTodasAsync(CancellationToken cancellationToken = default);
    Task<Cuenta?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Cuenta>> ObtenerPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default);
    Task<Cuenta?> ObtenerPorNumeroCuentaAsync(string numeroCuenta, CancellationToken cancellationToken = default);
    Task AgregarAsync(Cuenta cuenta, CancellationToken cancellationToken = default);
    void Actualizar(Cuenta cuenta);
    void Eliminar(Cuenta cuenta);
    Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
