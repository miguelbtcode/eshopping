namespace Basket.Application.Mappers;

using AutoMapper;

public static class BasketMapper
{
    private static readonly Lazy<IMapper> Lazy = new(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<BasketMappingProfile>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    
    public static IMapper Mapper => Lazy.Value;
}