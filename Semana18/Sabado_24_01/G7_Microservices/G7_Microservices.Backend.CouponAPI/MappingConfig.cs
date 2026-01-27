using AutoMapper;
using G7_Microservices.Backend.CouponAPI.Models;
using G7_Microservices.Backend.CouponAPI.Models.Dto;

namespace G7_Microservices.Backend.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Coupon, CouponDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
