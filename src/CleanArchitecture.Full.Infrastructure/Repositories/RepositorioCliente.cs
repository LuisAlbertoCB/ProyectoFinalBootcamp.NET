using CleanArchitecture.Full.Domain;
using CleanArchitecture.Full.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Full.Infrastructure.Repositories;

public class RepositorioCliente(AppDbContext contexto) : IRepositorioCliente
{
    public async Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await contexto.Clientes.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Cliente?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await contexto.Clientes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<Cliente?> ObtenerPorNumeroDocumentoAsync(string numeroDocumento, CancellationToken cancellationToken = default) =>
        await contexto.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.NumeroDocumento == numeroDocumento, cancellationToken);

    public async Task<Cliente?> ObtenerPorCorreoAsync(string correoElectronico, CancellationToken cancellationToken = default) =>
        await contexto.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.CorreoElectronico == correoElectronico, cancellationToken);

    public async Task AgregarAsync(Cliente cliente, CancellationToken cancellationToken = default) =>
        await contexto.Clientes.AddAsync(cliente, cancellationToken);

    public void Actualizar(Cliente cliente) => contexto.Clientes.Update(cliente);

    public void Eliminar(Cliente cliente) => contexto.Clientes.Remove(cliente);

    public async Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default) =>
        await contexto.SaveChangesAsync(cancellationToken) >= 0;
}
