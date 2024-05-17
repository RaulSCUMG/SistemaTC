using Entity = SistemaTC.Data.Entities.CreditCard;
using EntityDTO = SistemaTC.DTO.CreditCard.CreditCard;
using ExistingDTOPin = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using ExistingDTOBloqueo = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using ExistingDTOAumento = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using NewDTO = SistemaTC.DTO.CreditCard.CreditCardNew;
using ResponseDTOSaldos = SistemaTC.DTO.CreditCard.CreditCardResponseSaldo;
using ResponseDTOFecha = SistemaTC.DTO.CreditCard.CreditCardResponseFecha;
using ResponseDTODetalle = SistemaTC.DTO.CreditCard.CreditCardResponseDetalle;

namespace SistemaTC.Api.Profile;
public partial class AutoMapperProfile
{
    public void AddCreditCardMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOPin>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOBloqueo>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.LockedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOAumento>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, EntityDTO>()
            .ReverseMap();
    }
}
