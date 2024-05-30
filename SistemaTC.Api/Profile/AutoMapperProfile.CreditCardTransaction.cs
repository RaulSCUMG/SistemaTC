using Entity = SistemaTC.Data.Entities.CreditCardTransaction;
using EntityDTO = SistemaTC.DTO.CreditCardTransaction.CreditCardTransaction;
using NewDTO = SistemaTC.DTO.CreditCardTransaction.CreditCardTransactionNew;

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
