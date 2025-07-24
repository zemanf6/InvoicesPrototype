using AutoMapper;
using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>();

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Buyer.Id))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id))
                .ForMember(dest => dest.Buyer, opt => opt.Ignore())
                .ForMember(dest => dest.Seller, opt => opt.Ignore());
        }
    }
}
