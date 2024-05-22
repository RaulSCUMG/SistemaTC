using Entity = SistemaTC.Data.Entities.CreditCard;
using EntityDTO = SistemaTC.DTO.CreditCard.CreditCard;
using NewDTO = SistemaTC.DTO.CreditCard.CreditCardNew;

namespace SistemaTC.Api.Profile;
public partial class AutoMapperProfile
{
    public void AddCreditCardTransactionMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow));
        CreateMap<Entity, EntityDTO>()
            .ReverseMap();
    }
}
