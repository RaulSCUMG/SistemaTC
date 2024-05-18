using Entity = SistemaTC.Data.Entities.CreditCutOff;
using EntityDTO = SistemaTC.DTO.CreditCutoff.CreditCutoff;

namespace SistemaTC.Api.Profile;

public partial class AutoMapperProfile
{
    public void AddCreditCutoffMap()
    {
        CreateMap<Entity, EntityDTO>()
            .ReverseMap();
    }
}
