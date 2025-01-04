namespace Discount.Application.Handlers;

using Commands;
using Core.Repositories;
using MediatR;

public sealed class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository discountRepository;
    
    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        this.discountRepository = discountRepository;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        return await discountRepository.DeleteDiscountAsync(request.ProductName);
    }
}