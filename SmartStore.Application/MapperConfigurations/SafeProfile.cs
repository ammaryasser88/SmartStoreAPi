using AutoMapper;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Dtos.Response;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.MapperConfigurations
{
    public class SafeProfile :Profile
    {
        public SafeProfile()
        {
            CreateMap<SafeRequest, Safe>()
                .ForMember(dest => dest.IsDeleted, obj => obj.MapFrom(src => false))
                .ForMember(dest => dest.IsActive, obj => obj.MapFrom(src => true)).ReverseMap();

            CreateMap<Safe, SafeResponse>().ReverseMap();
        }
    }
}
