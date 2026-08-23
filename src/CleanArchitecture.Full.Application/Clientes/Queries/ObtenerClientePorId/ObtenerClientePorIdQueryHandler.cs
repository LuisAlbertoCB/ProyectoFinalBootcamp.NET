using CleanArchitecture.Full.Application.Dtos;
using CleanArchitecture.Full.Domain;
using MediatR;

namespace CleanArchitecture.Full.Application.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdQueryHandler(IRepositorioCliente repositorio) : IRequestHandler<ObtenerClientePorIdQuery, ClienteDto?>
{
    public async Task<ClienteDto?> Handle(ObtenerClientePorIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await repositorio.ObtenerPorIdAsync(request.Id, cancellationToken);
        return cliente?.ToDto();
    }
}
