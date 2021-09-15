using AutoMapper;
using Keopi.Service.Events;
using KeopiAPI.Helpers;
using KeopiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventsService _eventsService;

        public EventsController(IMapper mapper, IEventsService eventsService)
        {
            _mapper = mapper;
            _eventsService = eventsService;
        }

        [HttpGet]
        public IActionResult GetByDate([RequiredFromQuery] string dateTime, [FromQuery] string cafeId)
        {
            try
            {
                //01.01.2001.
                int day = int.Parse(dateTime.Substring(0, 2));
                int month = int.Parse(dateTime.Substring(3, 2));
                int year = int.Parse(dateTime.Substring(6, 4));
                var date = new DateTime(year, month, day);

                var events = _eventsService.GetByDate(date, cafeId);

                var eventViewModels = _mapper.Map<List<EventViewModel>>(events);

                return Ok(eventViewModels);
            }
            catch (Exception)
            {
                return BadRequest("Invalid parameters");
            }

        }

        [HttpGet("GetEventDates2MonthsBackAndUp")]
        public IActionResult GetEventDates2MonthsBackAndUp([RequiredFromQuery] int year, [RequiredFromQuery] int month,
            [FromQuery] string cafeId)
        {
            // check if params are valid
            try
            {
                var dateTime = new DateTime(year, month, 1);
            }
            catch (Exception)
            {
                return BadRequest("Invalid parameters");
            }

            var dates = _eventsService.GetEventDates2MonthsBackAndUp(year, month, cafeId);

            var stringDates = new List<string>();
            dates.ForEach(x => stringDates.Add(x.Date.ToLocalTime().ToString("dd.MM.yyyy.")));
            return Ok(stringDates);
        }
    }
}
