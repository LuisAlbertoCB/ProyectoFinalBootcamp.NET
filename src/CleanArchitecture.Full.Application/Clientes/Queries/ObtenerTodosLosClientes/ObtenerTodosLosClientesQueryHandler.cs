using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Queries.ObtenerTodosLosClientes;

public class ObtenerTodosLosClientesQueryHandler(IRepositorioCliente repositorio) : IRequestHandler<ObtenerTodosLosClientesQuery, IReadOnlyList<ClienteDto>>
{
    public async Task<IReadOnlyList<ClienteDto>> Handle(ObtenerTodosLosClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await repositorio.ObtenerTodosAsync(cancellationToken);
        return clientes.Select(c => c.ToDto()).ToList();
    }
}
