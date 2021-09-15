using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using FluentAssertions;
using Keopi.Service.Events;
using MongoDB.Bson;
using Keopi.Models;
using KeopiAPI.Models;
using KeopiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Keopi.WebAPITests
{
    public class EventsControllerTests
    {
        [Fact]
        public void GetByDate_WithValidDate_ReturnsOkAndEvents()
        {
            var mapperStub = new Mock<IMapper>();
            var serviceStub = new Mock<IEventsService>();

            var dateTime = DateTime.Today;

            var events = new List<Event>
            {
                new Event
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    CafeBarId = ObjectId.GenerateNewId().ToString(),
                    Date = dateTime
                }
            };

            serviceStub.Setup(x => x.GetByDate(dateTime, null))
                .Returns(events);

            var eventViewModels = new List<EventViewModel>
            {
                new EventViewModel
                {
                    Id = events[0].Id,
                    CafeBarId = events[0].CafeBarId,
                    Date = dateTime.ToString("dd.MM.yyyy. HH")
                }
            };

            mapperStub.Setup(x => x.Map<List<EventViewModel>>(events))
                .Returns(eventViewModels);

            var controller = new EventsController(mapperStub.Object, serviceStub.Object);

            var result = controller.GetByDate(dateTime.ToString("dd.MM.yyyy."), null) as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(
                eventViewModels,
                options => options.ComparingByMembers<EventViewModel>());
        }

        [Theory]
        [InlineData("ghfds")]
        [InlineData("1.1.2001.")]
        [InlineData("")]
        [InlineData("01.01.01.")]
        public void GetByDate_WithInvalidDate_ReturnsBadRequest(string invalidDateTimeString)
        {
            var mapperStub = new Mock<IMapper>();
            var serviceStub = new Mock<IEventsService>();

            var controller = new EventsController(mapperStub.Object, serviceStub.Object);

            var result = controller.GetByDate(invalidDateTimeString, null);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void GetEventDates2MonthsBackAndUp_ValidParams_ReturnsDates()
        {
            var mapperStub = new Mock<IMapper>();
            var serviceStub = new Mock<IEventsService>();

            var dateTimeNow = DateTime.Now;

            int year = dateTimeNow.Year;
            int month = dateTimeNow.Month;

            var dates = new List<DateTime>
            {
                dateTimeNow,
                dateTimeNow.AddDays(1),
                dateTimeNow.AddMonths(-1)
            };

            serviceStub.Setup(x => x.GetEventDates2MonthsBackAndUp(year, month, null))
                .Returns(dates);

            var stringDates = new List<string>();
            dates.ForEach(x => stringDates.Add(x.Date.ToLocalTime().ToString("dd.MM.yyyy.")));

            var controller = new EventsController(mapperStub.Object, serviceStub.Object);

            var result = controller.GetEventDates2MonthsBackAndUp(year, month, null) as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(
                stringDates,
                options => options.ComparingByMembers<string>());
        }

        [Theory]
        [InlineData(2021, 13)]
        [InlineData(-1, 12)]
        [InlineData(2000, -1)]
        public void GetEventDates2MonthsBackAndUp_InvalidParams_ReturnsBadRequest(int year, int month)
        {
            var mapperStub = new Mock<IMapper>();
            var serviceStub = new Mock<IEventsService>();

            var controller = new EventsController(mapperStub.Object, serviceStub.Object);

            var result = controller.GetEventDates2MonthsBackAndUp(year, month, null);

            result.Should().BeOfType<BadRequestObjectResult>();
            serviceStub.Verify(x => x.GetEventDates2MonthsBackAndUp(It.IsAny<int>(), It.IsAny<int>(), null), Times.Never);
        }
    }
}
