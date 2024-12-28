namespace Catalog.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository productRepository;
    
    public GetProductByNameQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await productRepository.GetProductsByNameAsync(request.Name);
        var productListResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return productListResponse;
    }
}