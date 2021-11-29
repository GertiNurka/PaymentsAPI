using AutoMapper;
using PaymentsAPI.Application.Dtos;
using PaymentsDomain.AggregatesModel.PaymentAggregate;

namespace PaymentsAPI.Application.Mapping
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Card.Name))
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.Card.CardNumber))
                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.Card.ExpiryDate))
                .ForMember(dest => dest.Cvv, opt => opt.MapFrom(src => src.Card.Cvv));
            CreateMap<BillingAddress, BillingAddressDto>().ReverseMap();
        }
    }
}
