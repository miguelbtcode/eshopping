namespace Catalog.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IList<ProductResponse>>
{
    private readonly IProductRepository productRepository;
    
    public GetAllProductsHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    
    public async Task<IList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetAllProducts();
        var productResponseList = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return productResponseList;
    }
}