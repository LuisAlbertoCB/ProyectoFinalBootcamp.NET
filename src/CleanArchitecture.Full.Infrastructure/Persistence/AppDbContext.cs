using CleanArchitecture.Full.Domain;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Full.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(80);
            entity.Property(c => c.Apellido).IsRequired().HasMaxLength(80);
            entity.Property(c => c.NumeroDocumento).IsRequired().HasMaxLength(30);
            entity.Property(c => c.CorreoElectronico).IsRequired().HasMaxLength(150);
            entity.Property(c => c.Telefono).IsRequired().HasMaxLength(30);
            entity.Property(c => c.FechaNacimiento).IsRequired().HasColumnType("date");
            entity.Property(c => c.Estado).IsRequired().HasMaxLength(20);
            entity.HasIndex(c => c.NumeroDocumento).IsUnique();
            entity.HasIndex(c => c.CorreoElectronico).IsUnique();
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.ToTable("cuentas");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.NumeroCuenta).IsRequired().HasMaxLength(20);
            entity.Property(c => c.TipoCuenta).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Moneda).IsRequired().HasMaxLength(3);
            entity.Property(c => c.Saldo).HasColumnType("numeric(18,2)");
            entity.Property(c => c.LimiteSobregiro).HasColumnType("numeric(18,2)");
            entity.Property(c => c.Estado).IsRequired().HasMaxLength(20);
            entity.HasIndex(c => c.NumeroCuenta).IsUnique();
            entity.HasOne(c => c.Cliente)
                .WithMany(cl => cl.Cuentas)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
