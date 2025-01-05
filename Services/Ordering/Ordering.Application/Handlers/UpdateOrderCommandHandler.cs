namespace Ordering.Application.Handlers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Repositories;
using Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<UpdateOrderCommandHandler> logger;
    
    public UpdateOrderCommandHandler(
        IOrderRepository orderRepository, 
        IMapper mapper, 
        ILogger<UpdateOrderCommandHandler> logger)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate == null)
            throw new OrderNotFoundException(nameof(Order), request.Id);

        mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
        await orderRepository.UpdateAsync(orderToUpdate);
        logger.LogInformation("Order {OrderId} is successfully updated.", orderToUpdate.Id);
        return Unit.Value;
    }
}