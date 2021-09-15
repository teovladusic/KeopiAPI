using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using Keopi.Service.Cities;
using Keopi.Models;
using MongoDB.Bson;
using KeopiAPI.Models;
using KeopiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Keopi.WebAPITests
{
    public class CitiesControllerTests
    {
        [Fact]
        public void GetAll_WithValidParams_ReturnsOkAndCities()
        {
            var mapperStub = new Mock<IMapper>();
            var citiesServiceStub = new Mock<ICitiesService>();

            var cityId = ObjectId.GenerateNewId();
            var cities = new List<City> { new City { Id = cityId.ToString(), Name = "city" } };

            citiesServiceStub.Setup(x => x.GetAll())
                .Returns(cities);

            var cityViewModels = new List<CityViewModel> { new CityViewModel { Id = cityId.ToString(), Name = "city" } };

            mapperStub.Setup(x => x.Map<List<CityViewModel>>(cities))
                .Returns(cityViewModels);

            var controller = new CitiesController(mapperStub.Object, citiesServiceStub.Object);

            var result = controller.GetAll() as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().BeEquivalentTo(
                cityViewModels,
                options => options.ComparingByMembers<CityViewModel>());
        }
    }
}
