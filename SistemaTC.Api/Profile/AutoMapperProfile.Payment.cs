using Entity = SistemaTC.Data.Entities.Payment;
using EntityDTO = SistemaTC.DTO.Payment.Payment;
using NewDTO = SistemaTC.DTO.Payment.PaymentNew;

namespace SistemaTC.Api.Profile;
public partial class AutoMapperProfile
{
    public void AddPaymentMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, EntityDTO>()
            .ReverseMap();
    }
}
