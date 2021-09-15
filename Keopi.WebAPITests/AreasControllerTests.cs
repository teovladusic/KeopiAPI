using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using AutoMapper;
using Keopi.Service.Areas;
using MongoDB.Bson;
using KeopiAPI.Models;
using Keopi.Models;
using KeopiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Keopi.WebAPITests
{
    public class AreasControllerTests
    {

        [Fact]
        public void GetAllByCityId_WithValidId_ReturnsOkAndAreas()
        {
            var mapperStub = new Mock<IMapper>();
            var areasServiceStub = new Mock<IAreasService>();

            var cityId = ObjectId.GenerateNewId();

            var areas = new List<Area>
            {
                new Area { Id = ObjectId.GenerateNewId().ToString(), CityId = cityId.ToString(), Name = "name"}
            };

            areasServiceStub.Setup(x => x.GetByCityId(cityId.ToString()))
                .Returns(areas);

            var areaViewModels = new List<AreaViewModel>
            {
                new AreaViewModel { Id = areas[0].Id.ToString(), CityId = cityId.ToString(), Name = "name"}
            };

            mapperStub.Setup(x => x.Map<List<AreaViewModel>>(areas))
                .Returns(areaViewModels);

            var controller = new AreasController(mapperStub.Object, areasServiceStub.Object);

            var result = controller.GetByCityId(cityId.ToString()) as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(
                areaViewModels,
                options => options.ComparingByMembers<AreaViewModel>());
        }

        [Fact]
        public void GetAllByCityId_WithInvalidId_ReturnsBadRequest()
        {
            var mapperStub = new Mock<IMapper>();
            var areasServiceStub = new Mock<IAreasService>();

            string cityId = null;

            var controller = new AreasController(mapperStub.Object, areasServiceStub.Object);

            var result = controller.GetByCityId(cityId);

            result.Should().BeOfType<BadRequestResult>();
            areasServiceStub.Verify(x => x.GetByCityId(It.IsAny<string>()),
                Times.Never);
            mapperStub.Verify(x => x.Map<List<AreaViewModel>>(It.IsAny<Area>()),
                Times.Never);
        }
    }
}
