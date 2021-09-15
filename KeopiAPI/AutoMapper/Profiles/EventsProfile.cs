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
    public class EventsProfile : Profile
    {
        public EventsProfile()
        {
            CreateMap<Event, EventDbModel>();
            CreateMap<EventDbModel, Event>();


            CreateMap<Event, EventViewModel>()
                .ForMember(dest => dest.Date,
                options => options.MapFrom(source => source.Date.ToLocalTime().ToString("dd.MM.yyyy. HH")));
        }
    }
}
