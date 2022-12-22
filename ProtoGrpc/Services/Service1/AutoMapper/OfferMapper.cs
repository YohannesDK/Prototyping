using AutoMapper;
using Service1.Entities;
using Protos.Service1;

namespace Service1.AutoMapper
{
    public class OfferMapper : Profile
    {
        public OfferMapper()
        {
            CreateMap<Offer, OfferDetail>().ReverseMap();
            CreateMap<MegaExtendedOfferDetail, Offer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MegaOfferDetail.OfferDetail.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.MegaOfferDetail.OfferDetail.ProductName))
                .ForMember(dest => dest.OfferDescription, opt => opt.MapFrom(src => src.MegaOfferDetail.OfferDetail.OfferDescription))
                .ReverseMap();
        }
    }
}
