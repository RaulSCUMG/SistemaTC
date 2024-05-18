namespace SistemaTC.Api.Profile;

public partial class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        AddUserMap();
        AddRoleMap();
        AddRequestMap();
        AddCreditCardMap();
        AddCreditCutoffMap();
    }
}
