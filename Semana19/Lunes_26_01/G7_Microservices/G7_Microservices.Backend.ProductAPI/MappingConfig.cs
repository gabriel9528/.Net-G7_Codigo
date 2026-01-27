using AutoMapper;
using G7_Microservices.Backend.ProductAPI.Models;
using G7_Microservices.Backend.ProductAPI.Models.Dto;

namespace G7_Microservices.Backend.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
