namespace Catalog.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository productRepository;
    
    public GetProductsByBrandQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProductsByBrandNameAsync(request.BrandName);
        var productListResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return productListResponse;
    }
}