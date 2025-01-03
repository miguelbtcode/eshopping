namespace Catalog.Application.Handlers;

using Core.Repositories;
using Core.Specs;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository productRepository;
    
    public GetAllProductsHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetAllProducts(request.CatalogSpecParams);
        var productListResponse = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
        return productListResponse;
    }
}