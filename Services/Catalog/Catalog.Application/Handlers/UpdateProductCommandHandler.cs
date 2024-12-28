namespace Catalog.Application.Handlers;

using Commands;
using Core.Entities;
using Core.Repositories;
using MediatR;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository productRepository;
    
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updatedResult = await productRepository.UpdateProductAsync(new Product
        {
            Id = request.Id,
            Name = request.Name,
            Summary = request.Summary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
            Brand = request.Brand,
            Type = request.Type
        });

        return updatedResult;
    }
}