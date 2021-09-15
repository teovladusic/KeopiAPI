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
    public class AreasProfile : Profile
    {
        public AreasProfile()
        {
            CreateMap<AreaDbModel, Area>();
            CreateMap<Area, AreaDbModel>();

            CreateMap<Area, AreaViewModel>();
            CreateMap<AreaViewModel, Area>();
        }
    }
}
