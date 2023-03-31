using AutoMapper;
using WebApiTienda.DTOs;
using WebApiTienda.Helpers;
using WebApiTienda.Models;

namespace WebApiTienda
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<FakeStoreProduct, ProductDTO>()
                .ForMember(d => d.Thumbnail, o => o.MapFrom(s => s.Image));

            CreateMap<DummyJsonProduct, ProductDTO>()
                .ForPath(d => d.Rating.Rate, o => o.MapFrom(s => s.Rating));

            CreateMap<PurchaseDetail, PurchaseDetailRequest>().ReverseMap();

            CreateMap<Purchase, PurchaseRequest>().ReverseMap();
        }
    }
}
