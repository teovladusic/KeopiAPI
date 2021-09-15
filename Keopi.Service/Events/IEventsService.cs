using Keopi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Keopi.Service.Events
{
    public interface IEventsService
    {
        public List<Event> GetByDate(DateTime dateTime, string cafeId);

        public List<DateTime> GetEventDates2MonthsBackAndUp(int year, int month, string cafeId);
    }
}