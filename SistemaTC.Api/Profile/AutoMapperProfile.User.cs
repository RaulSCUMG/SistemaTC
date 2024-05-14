using Entity = SistemaTC.Data.Entities.User;
using EntityDTO = SistemaTC.DTO.User.User;
using ExistingDTO = SistemaTC.DTO.User.ExistingUser;
using NewDTO = SistemaTC.DTO.User.NewUser;

namespace SistemaTC.Api.Profile;

public partial class AutoMapperProfile
{
    public void AddUserMap()
    {
        CreateMap<Entity, NewDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User));
        CreateMap<Entity, ExistingDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.User));
        CreateMap<Entity, EntityDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UpdatedBy ?? src.CreatedBy))
            .ReverseMap();
    }
}
