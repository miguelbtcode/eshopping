namespace Ordering.Application.Commands;

using MediatR;

public sealed record DeleteOrderCommand(int Id) : IRequest<Unit>;