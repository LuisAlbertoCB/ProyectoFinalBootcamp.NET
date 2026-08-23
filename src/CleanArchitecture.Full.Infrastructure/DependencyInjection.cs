using CleanArchitecture.Full.Domain;
using CleanArchitecture.Full.Infrastructure.Persistence;
using CleanArchitecture.Full.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Full.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AgregarInfraestructura(this IServiceCollection services, IConfiguration configuration)
    {
        var cadenaConexion = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AppDbContext>(options => options
            .UseNpgsql(cadenaConexion)
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IRepositorioCuenta, RepositorioCuenta>();
        services.AddScoped<IRepositorioCliente, RepositorioCliente>();
        return services;
    }
}
