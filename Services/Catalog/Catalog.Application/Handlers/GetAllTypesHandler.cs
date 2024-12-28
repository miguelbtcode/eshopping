namespace Catalog.Application.Handlers;

using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
{
    private readonly IProductTypeRepository productTypeRepository;
    
    public GetAllTypesHandler(IProductTypeRepository productTypeRepository)
    {
        this.productTypeRepository = productTypeRepository;
    }

    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await productTypeRepository.GetAllProductTypesAsync();
        var typeListResponse = ProductMapper.Mapper.Map<List<TypesResponse>>(typeList);
        return typeListResponse;
    }
}