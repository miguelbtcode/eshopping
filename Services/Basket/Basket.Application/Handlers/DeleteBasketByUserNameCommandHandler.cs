namespace Basket.Application.Handlers;

using Commands;
using Core.Repositories;
using MediatR;

public sealed class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    private readonly IBasketRepository basketRepository;

    public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
    {
        this.basketRepository = basketRepository;
    }
    
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasketAsync(request.UserName);
        return Unit.Value;
    }
}