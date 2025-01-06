namespace Ordering.Application.Handlers;

using AutoMapper;
using Core.Repositories;
using MediatR;
using Queries;
using Responses;

public sealed class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<OrderResponse>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;
    
    public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<List<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await orderRepository.GetOrdersByUserName(request.UserName);
        return mapper.Map<List<OrderResponse>>(orderList);
    }
}