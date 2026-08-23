namespace CleanArchitecture.Full.Domain;

public interface IRepositorioCliente
{
    Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<Cliente?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Cliente?> ObtenerPorNumeroDocumentoAsync(string numeroDocumento, CancellationToken cancellationToken = default);
    Task<Cliente?> ObtenerPorCorreoAsync(string correoElectronico, CancellationToken cancellationToken = default);
    Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken = default);
    void Actualizar(Cliente cliente);
    void Eliminar(Cliente cliente);
    Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default);
}
