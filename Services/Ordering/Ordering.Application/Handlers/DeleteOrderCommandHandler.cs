namespace Ordering.Application.Handlers;

using Commands;
using Core.Entities;
using Core.Repositories;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> logger;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        this.orderRepository = orderRepository;
        this.logger = logger;
    }
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
        if (orderToDelete == null)
            throw new OrderNotFoundException(nameof(Order), request.Id);
        
        await orderRepository.DeleteAsync(orderToDelete);
        logger.LogInformation("Order with Id {OrderId} is deleted successfully.", request.Id);
        return Unit.Value;
    }
}