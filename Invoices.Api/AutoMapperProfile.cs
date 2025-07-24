using AutoMapper;
using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }
    }
}
