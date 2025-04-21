using AutoMapper;
using SmartStore.Domain.Dtos.Request;
using SmartStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Application.MapperConfigurations
{
    public class RevenueProfile : Profile
    {
        public RevenueProfile()
        {
            CreateMap<RevenueRequest, Revenue>()
               .ForMember(dest => dest.IsDeleted, obj => obj.MapFrom(src => false))
               .ForMember(dest => dest.IsActive, obj => obj.MapFrom(src => true)).ReverseMap();
        }
    }
}
