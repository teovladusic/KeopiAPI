using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Events
{
    public class EventsService : IEventsService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EventDbModel> _repository;

        public EventsService(IMapper mapper, IGenericRepository<EventDbModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public List<Event> GetByDate(DateTime dateTime, string cafeId)
        {
            List<Event> events;
            if (cafeId is null)
            {
                var eventDbModels = _repository.FilterBy(x => x.Date >= dateTime && x.Date < dateTime.AddHours(24));
                events = _mapper.Map<List<Event>>(eventDbModels);
            }
            else
            {
                var eventDbModels = _repository.FilterBy(x => x.Date >= dateTime && x.Date < dateTime.AddHours(24) 
                && x.CafeBarId == cafeId);

                events = _mapper.Map<List<Event>>(eventDbModels);
            }
            return events;
        }

        public List<DateTime> GetEventDates2MonthsBackAndUp(int year, int month, string cafeId)
        {
            var currentMonth = new DateTime(year, month, 1);
            var minDate = currentMonth.AddMonths(-2);
            var maxDate = currentMonth.AddMonths(3).AddMinutes(-1);

            IEnumerable<DateTime> events;

            if (cafeId is null)
            {
                events = _repository.FilterBy(x =>
                x.Date >= minDate
                && x.Date <= maxDate,
                    x => x.Date);
            }
            else
            {
                events = _repository.FilterBy(x => x.Date >= minDate && x.Date <= maxDate && x.CafeBarId == cafeId,
                    x => x.Date);
            }
            return events.ToList();
        }
    }
}
