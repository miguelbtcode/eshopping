namespace Ordering.Application.Handlers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

public class CheckoutOrderCommandHandlerV2 : IRequestHandler<CheckoutOrderCommandV2, int>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    private readonly ILogger<CheckoutOrderCommandHandler> logger;

    public CheckoutOrderCommandHandlerV2(
        IOrderRepository orderRepository, 
        IMapper mapper, 
        ILogger<CheckoutOrderCommandHandler> logger)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<int> Handle(CheckoutOrderCommandV2 request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<Order>(request);
        var generatedOrder = await orderRepository.AddAsync(orderEntity);
        logger.LogInformation("Order {OrderId} successfully created.", generatedOrder);
        return generatedOrder.Id;
    }
}