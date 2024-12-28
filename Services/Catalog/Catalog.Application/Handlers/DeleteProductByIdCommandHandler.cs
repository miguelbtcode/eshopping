namespace Catalog.Application.Handlers;

using Commands;
using Core.Repositories;
using MediatR;

public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository productRepository;
    
    public DeleteProductByIdCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        return await productRepository.DeleteProductAsync(request.Id);
    }
}