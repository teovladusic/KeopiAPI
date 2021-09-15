using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.AutoMapper.Profiles
{
    public class PromoCafesProfile : Profile
    {
        public PromoCafesProfile()
        {
            CreateMap<PromoCafeBarDbModel, PromoCafe>();
            CreateMap<PromoCafe, PromoCafeBarDbModel>();
        }
    }
}
