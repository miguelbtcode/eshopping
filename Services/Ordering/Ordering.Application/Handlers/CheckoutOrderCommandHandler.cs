namespace Ordering.Application.Handlers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> logger;

    public CheckoutOrderCommandHandler(
        IOrderRepository orderRepository, 
        IMapper mapper, 
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<Order>(request);
        var generatedOrder = await orderRepository.AddAsync(orderEntity);
        logger.LogInformation("Order {OrderId} successfully created.", generatedOrder);
        return generatedOrder.Id;
    }
}