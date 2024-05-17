using Entity = SistemaTC.Data.Entities.CreditCard;
using EntityDTO = SistemaTC.DTO.CreditCard.CreditCard;
using ExistingDTOPin = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using ExistingDTOBloqueo = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using ExistingDTOAumento = SistemaTC.DTO.CreditCard.ExistingCreditCardPin;
using NewDTO = SistemaTC.DTO.CreditCard.CreditCardNew;

namespace SistemaTC.Api.Profile;
public partial class AutoMapperProfile
{
    public void AddCreditCardMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User));
        CreateMap<Entity, ExistingDTOPin>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.User));
        CreateMap<Entity, ExistingDTOBloqueo>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.LockedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, ExistingDTOAumento>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.User));
        CreateMap<Entity, EntityDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy))
            .ReverseMap();
    }
}
