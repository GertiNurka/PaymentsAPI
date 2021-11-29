using AutoMapper;
using PaymentsAPI.Application.Dtos;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsAPI.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<Card, CardDto>().ReverseMap();
            CreateMap<BillingAddress, BillingAddressDto>().ReverseMap();
        }
    }
}
