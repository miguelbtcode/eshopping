namespace Catalog.Application.Handlers;

using AutoMapper;
using Core.Repositories;
using Mappers;
using MediatR;
using Queries;
using Responses;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IProductBrandRepository brandRepository;
    
    public GetAllBrandsHandler(IProductBrandRepository brandRepository)
    {
        this.brandRepository = brandRepository;
    }
    
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await brandRepository.GetAllProductBrandsAsync();
        var brandListResponse = ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
        return brandListResponse;
    }
}