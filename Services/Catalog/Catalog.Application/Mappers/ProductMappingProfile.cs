namespace Catalog.Application.Mappers;

using AutoMapper;
using Commands;
using Core.Entities;
using Core.Specs;
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
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name)).ReverseMap();

        CreateMap<Product, CreateProductCommand>()
            .ReverseMap();

        CreateMap<Pagination<Product>, Pagination<ProductResponse>>()
            .ReverseMap();
        
        CreateMap<Product, UpdateProductCommand>()
            .ReverseMap();
    }
}