namespace Ordering.API.EventBusConsumer;

using Application.Commands;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

public sealed class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    private readonly ILogger<BasketOrderingConsumer> logger;

    public BasketOrderingConsumer(
        IMediator mediator, 
        IMapper mapper,
        ILogger<BasketOrderingConsumer> logger)
    {
        this.mediator = mediator;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = logger.BeginScope("Consuming Basket Checkout Event for {CorrelationId}", context.Message.CorrelationId);
        var cmd = mapper.Map<CheckoutOrderCommand>(context.Message);
        await mediator.Send(cmd);
        logger.LogInformation("Basket Checkout Event completed!!!");
    }
}