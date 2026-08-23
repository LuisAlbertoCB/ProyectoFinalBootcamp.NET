using CleanArchitecture.Full.Domain;
using CleanArchitecture.Full.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Full.Infrastructure.Repositories;

public class RepositorioCuenta(AppDbContext contexto) : IRepositorioCuenta
{
    public async Task<IReadOnlyList<Cuenta>> ObtenerTodasAsync(CancellationToken cancellationToken = default) =>
        await contexto.Cuentas.AsNoTracking().Include(c => c.Cliente).ToListAsync(cancellationToken);

    public async Task<Cuenta?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await contexto.Cuentas.Include(c => c.Cliente).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Cuenta>> ObtenerPorClienteIdAsync(Guid clienteId, CancellationToken cancellationToken = default) =>
        await contexto.Cuentas.AsNoTracking().Include(c => c.Cliente)
            .Where(c => c.ClienteId == clienteId)
            .ToListAsync(cancellationToken);

    public async Task<Cuenta?> ObtenerPorNumeroCuentaAsync(string numeroCuenta, CancellationToken cancellationToken = default) =>
        await contexto.Cuentas.AsNoTracking().FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta, cancellationToken);

    public async Task AgregarAsync(Cuenta cuenta, CancellationToken cancellationToken = default) =>
        await contexto.Cuentas.AddAsync(cuenta, cancellationToken);

    public void Actualizar(Cuenta cuenta) => contexto.Cuentas.Update(cuenta);

    public void Eliminar(Cuenta cuenta) => contexto.Cuentas.Remove(cuenta);

    public async Task<bool> GuardarCambiosAsync(CancellationToken cancellationToken = default) =>
        await contexto.SaveChangesAsync(cancellationToken) >= 0;
}
