using AutoMapper;

namespace Northwind.EntityModels;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CategoryDto, Category>();
            config.CreateMap<Category, CategoryDto>();

            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        });

        return mappingConfig;
    }
}

