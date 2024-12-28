namespace Catalog.Application.Mappers;

using AutoMapper;
using Commands;
using Core.Entities;
using Responses;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand, BrandResponse>()
            .ReverseMap();
        
        CreateMap<Product, ProductResponse>()
            .ReverseMap();
        
        CreateMap<ProductType, TypesResponse>()
            .ReverseMap();

        CreateMap<Product, CreateProductCommand>()
            .ReverseMap();
        
        CreateMap<Product, UpdateProductCommand>()
            .ReverseMap();
    }
}