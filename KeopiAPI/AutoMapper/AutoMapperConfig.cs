using AutoMapper;
using KeopiAPI.AutoMapper;
using KeopiAPI.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CafesProfile());
                mc.AddProfile(new PromoCafesProfile());
                mc.AddProfile(new CitiesProfile());
                mc.AddProfile(new AreasProfile());
                mc.AddProfile(new EventsProfile());
            });
            return mapperConfig.CreateMapper();
        }
    }
}
