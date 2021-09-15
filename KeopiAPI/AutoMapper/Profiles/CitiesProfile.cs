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
    public class CitiesProfile : Profile
    {
        public CitiesProfile()
        {
            CreateMap<City, CityDbModel>();
            CreateMap<CityDbModel, City>();

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>();
        }
    }
}
