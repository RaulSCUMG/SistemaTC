using Entity = SistemaTC.Data.Entities.Role;
using EntityDTO = SistemaTC.DTO.Role.Role;
using ExistingDTO = SistemaTC.DTO.Role.ExistingRole;
using NewDTO = SistemaTC.DTO.Role.NewRole;

namespace SistemaTC.Api.Profile;

public partial class AutoMapperProfile
{
    public void AddRoleMap()
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
