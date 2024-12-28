namespace Catalog.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetProductByBrandQueryHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository productRepository;
    
    public GetProductByBrandQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProductsByBrandNameAsync(request.BrandName);
        var productListResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return productListResponse;
    }
}