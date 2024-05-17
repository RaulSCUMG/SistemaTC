﻿using Entity = SistemaTC.Data.Entities.Request;
using EntityDTO = SistemaTC.DTO.Request.Request;
using UpdateDTO = SistemaTC.DTO.Request.UpdateRequest;
using NewDTO = SistemaTC.DTO.Request.NewRequest;

namespace SistemaTC.Api.Profile
{
    public partial class AutoMapperProfile
    {
        public void AddRequestMap()
        {
            CreateMap<Entity, NewDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.RequestedByUserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Entity, UpdateDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.User));
            CreateMap<Entity, EntityDTO>()
                .ReverseMap();
        }
    }
}
