using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using KeopiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.AutoMapper.Profiles
{
    public class CafesProfile : Profile
    {
        public CafesProfile()
        {
            CreateMap<Cafe, CafeViewModel>();
            CreateMap<CafeViewModel, Cafe>();

            CreateMap<PagedList<Cafe>, ListCafesViewModel>()
                .ForMember(dest => dest.Cafes,
                options => options.MapFrom(source =>
                source))
                .ForMember(dest => dest.CurrentPage,
                options => options.MapFrom(source =>
                source.CurrentPage))
                .ForMember(dest => dest.TotalPages,
                options => options.MapFrom(source =>
                source.TotalPages))
                .ForMember(dest => dest.PageSize,
                options => options.MapFrom(source =>
                source.PageSize));

            CreateMap<Cafe, CafeBarDbModel>();
            CreateMap<CafeBarDbModel, Cafe>();
        }
    }
}
