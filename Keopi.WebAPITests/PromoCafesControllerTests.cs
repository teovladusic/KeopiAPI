using AutoMapper;
using FluentAssertions;
using Keopi.Models;
using Keopi.Service.PromoCafes;
using KeopiAPI.Controllers;
using KeopiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Keopi.WebAPITests
{
    public class PromoCafesControllerTests
    {
        [Fact]
        public async Task GetAll_WithValidParams_ReturnsCafes()
        {
            var mapperStub = new Mock<IMapper>();
            var promoCafesServiceStub = new Mock<IPromoCafesService>();

            var promoCafes = new List<Cafe> { new Cafe { Id = "id", Name = "name" } };

            promoCafesServiceStub.Setup(x => x.GetAll())
                .ReturnsAsync(promoCafes);

            var promoCafeViewModels = new List<CafeViewModel> { new CafeViewModel { Id = "id", Name = "name" } };

            mapperStub.Setup(x => x.Map<List<CafeViewModel>>(promoCafes))
                .Returns(promoCafeViewModels);

            var controller = new PromoCafesController(mapperStub.Object, promoCafesServiceStub.Object);

            var result = await controller.GetAll() as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().BeEquivalentTo(
                promoCafeViewModels,
                options => options.ComparingByMembers<CafeViewModel>());
        }
    }
}
