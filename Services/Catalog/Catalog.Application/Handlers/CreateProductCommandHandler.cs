namespace Catalog.Application.Handlers;

using Commands;
using Core.Entities;
using Core.Repositories;
using Mappers;
using MediatR;
using Responses;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository productRepository;
    
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productRequest = ProductMapper.Mapper.Map<Product>(request);

        if (productRequest is null)
        {
            throw new ApplicationException("There is an issue with mapping while creating new product.");
        }
        
        var newProduct = await productRepository.CreateProductAsync(productRequest);
        var newProductResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);
        return newProductResponse;
    }
}