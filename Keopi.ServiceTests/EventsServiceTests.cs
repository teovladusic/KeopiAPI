using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Repository;
using MongoDB.Bson;
using Keopi.Models;
using Keopi.Service.Events;
using MongoDB.Driver;

namespace Keopi.ServiceTests
{
    public class EventsServiceTests
    {
        [Fact]
        public void GetByDate_WithValidDate_ReturnsEvents()
        {
            var mapperStub = new Mock<IMapper>();
            var repositoryStub = new Mock<IGenericRepository<EventDbModel>>();

            var dateTime = DateTime.Now;

            var eventDbModels = new List<EventDbModel>
            {
                new EventDbModel
                {
                    Id = ObjectId.GenerateNewId(),
                    CafeBarId = ObjectId.GenerateNewId().ToString(),
                    Date = dateTime
                }
            };

            repositoryStub.Setup(x => x.FilterBy(x => x.Date >= dateTime && x.Date < dateTime.AddHours(24)))
                .Returns(eventDbModels);

            var events = new List<Event>
            {
                new Event
                {
                    Id = eventDbModels[0].Id.ToString(),
                    CafeBarId = eventDbModels[0].CafeBarId,
                    Date = dateTime
                }
            };

            mapperStub.Setup(x => x.Map<List<Event>>(eventDbModels))
                .Returns(events);

            var service = new EventsService(mapperStub.Object, repositoryStub.Object);

            var result = service.GetByDate(dateTime, null);

            result.Should().BeEquivalentTo(
                events,
                options => options.ComparingByMembers<Event>());
        }

        [Fact]
        public void GetByDate_CafeIdNotNull_FiltersByCafeId()
        {
            var mapperStub = new Mock<IMapper>();
            var repositoryStub = new Mock<IGenericRepository<EventDbModel>>();

            var service = new EventsService(mapperStub.Object, repositoryStub.Object);

            var dateTime = DateTime.Today;
            var cafeId = "test_cafeId";

            service.GetByDate(dateTime, cafeId);

            repositoryStub.Verify(x => x.FilterBy(x => x.Date >= dateTime && x.Date < dateTime.AddHours(24)
                && x.CafeBarId == cafeId), Times.Once);
        }

        [Fact]
        public void GetEventDates2MonthsBackAndUp_ValidParams_ReturnsDateTimeList()
        {
            var mapperStub = new Mock<IMapper>();
            var repositoryStub = new Mock<IGenericRepository<EventDbModel>>();

            int year = 2021;
            int month = 9;

            var minDate = new DateTime(2021, 7, 1);
            var maxDate = new DateTime(2021, 11, 30).AddHours(23).AddMinutes(59);

            var dateTimeNow = DateTime.Now;

            var dates = new List<DateTime>
            {
                dateTimeNow,
                dateTimeNow.AddDays(1),
                dateTimeNow.AddMonths(-1)
            };

            repositoryStub.Setup(x => x.FilterBy(x => x.Date >= minDate && x.Date <= maxDate,
                x => x.Date)).Returns(dates);

            var service = new EventsService(mapperStub.Object, repositoryStub.Object);

            var result = service.GetEventDates2MonthsBackAndUp(year, month, null);

            Assert.Equal(dates, result);
        }

        [Fact]
        public void GetEventDates2MonthsBackAndUp_CafeIdNotNull_FiltersByCafeId()
        {
            var mapperStub = new Mock<IMapper>();
            var repositoryStub = new Mock<IGenericRepository<EventDbModel>>();

            var year = 2021;
            var month = 9;
            var cafeId = "test_cafeId";

            var currentMonth = new DateTime(year, month, 1);
            var minDate = currentMonth.AddMonths(-2);
            var maxDate = currentMonth.AddMonths(3).AddMinutes(-1);

            var service = new EventsService(mapperStub.Object, repositoryStub.Object);

            var result = service.GetEventDates2MonthsBackAndUp(year, month, cafeId);

            repositoryStub.Verify(x => x.FilterBy(x => x.Date >= minDate && x.Date <= maxDate && x.CafeBarId == cafeId,
                    x => x.Date), Times.Once);
        }
    }
}
