using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using AutoMapper;
using Keopi.Repository;
using Keopi.DataAccess.Models;
using MongoDB.Bson;
using Keopi.Models;
using Keopi.Service.Areas;

namespace Keopi.ServiceTests
{
    public class AreasServiceTests
    {
        [Fact]
        public void GetAllByCityId_WithExistingAreas_ReturnsAreas()
        {
            var mapperStub = new Mock<IMapper>();
            var repositoryStub = new Mock<IGenericRepository<AreaDbModel>>();

            var cityId = ObjectId.GenerateNewId().ToString();

            var areaDbModels = new List<AreaDbModel>
            {
                new AreaDbModel { Id = ObjectId.GenerateNewId(), CityId = cityId, Name = "name"}
            };

            repositoryStub.Setup(x => x.FilterBy(x => x.CityId == cityId))
                .Returns(areaDbModels);

            var areas = new List<Area>
            {
                new Area { Id = areaDbModels[0].Id.ToString(), CityId = cityId, Name = "name"}
            };

            mapperStub.Setup(x => x.Map<List<Area>>(areaDbModels))
                .Returns(areas);


            var service = new AreasService(mapperStub.Object, repositoryStub.Object);

            var result = service.GetByCityId(cityId.ToString());

            result.Should().BeEquivalentTo(
                areas,
                options => options.ComparingByMembers<Area>());
        }


    }
}
