using MediatR;

namespace CleanArchitecture.Full.Application.Cuentas.Commands.EliminarCuenta;

public record EliminarCuentaCommand(Guid Id) : IRequest<bool>;
