using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Keopi.Repository;
using Keopi.DataAccess.Models;
using AutoMapper;
using MongoDB.Bson;
using Keopi.Models;
using Keopi.Service.Cities;

namespace Keopi.ServiceTests
{
    public class CityServiceTests
    {
        public object BsonId { get; private set; }

        [Fact]
        public void GetAll_WithValidParams_ReturnsCities()
        {
            var citiesRepositoryStub = new Mock<IGenericRepository<CityDbModel>>();
            var mapperStub = new Mock<IMapper>();

            var cityId = ObjectId.GenerateNewId();
            var citiesDbModels = new List<CityDbModel> { new CityDbModel { Id = cityId, Name = "city" } };

            citiesRepositoryStub.Setup(x => x.AsQueryable())
                .Returns(citiesDbModels.AsQueryable());

            var cities = new List<City> { new City { Id = cityId.ToString(), Name = "city" } };

            mapperStub.Setup(x => x.Map<List<City>>(citiesDbModels))
                .Returns(cities);

            var service = new CitiesService(mapperStub.Object, citiesRepositoryStub.Object);

            var result = service.GetAll();

            result.Should().BeEquivalentTo(
                cities,
                options => options.ComparingByMembers<City>());
        }
    }
}
