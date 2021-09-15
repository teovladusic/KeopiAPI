using Keopi.Models;
using KeopiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Keopi.Service.Cafes;
using KeopiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using AutoMapper;
using Keopi.Models.Params;
using MongoDB.Bson;

namespace Keopi.WebAPITests
{
    public class CafesControllerTests
    {
        [Fact]
        public async Task GetAll_WithValidParameters_ReturnsOk()
        {
            var cafeParams = new CafeParams
            {
                Billiards = true,
                Betting = true,
                Seed = 543
            };

            var pagingParams = new PagingParams
            {
                CurrentPage = 1,
                PageSize = 12
            };

            var allCafes = new List<Cafe> { new Cafe { Name = "name" } };
            var pagedList = new PagedList<Cafe>(allCafes, allCafes.Count, pagingParams.CurrentPage, pagingParams.PageSize);

            var cafeViewModels = new List<CafeViewModel> { new CafeViewModel { Name = "name" } };

            var cafesServiceStub = new Mock<ICafesService>();
            cafesServiceStub.Setup(x => x.GetAll(cafeParams, pagingParams))
                .ReturnsAsync(pagedList);

            var listViewModel = new ListCafesViewModel
            {
                Cafes = cafeViewModels,
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                TotalPages = pagedList.TotalPages
            };

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(x => x.Map<ListCafesViewModel>(pagedList))
                .Returns(listViewModel);

            var controller = new CafesController(cafesServiceStub.Object, mapperStub.Object);
            var result = await controller.GetAll(cafeParams, pagingParams) as OkObjectResult;

            result.Value.Should().BeEquivalentTo(
                listViewModel,
                options => options.ComparingByMembers<PagedList<Cafe>>());
        }

        [Fact]
        public async Task GetAll_WithInavalidSeedValue_ReturnsBadRequest()
        {
            var cafeParams = new CafeParams
            {
                Billiards = true,
                Betting = true,
                Seed = 0
            };

            var pagingParams = new PagingParams
            {
                CurrentPage = 1,
                PageSize = 12
            };

            var cafesServiceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            var controller = new CafesController(cafesServiceStub.Object, mapperStub.Object);
            var result = await controller.GetAll(cafeParams, pagingParams);

            result.Should().BeOfType<BadRequestObjectResult>();
            cafesServiceStub.Verify(x => x.GetAll(It.IsAny<CafeParams>(), It.IsAny<PagingParams>()), Times.Never);
        }

        [Fact]
        public async Task GetAll_WithStartingAndEndingWorkTimeFiltered_ReturnsBadRequest()
        {
            var cafesServiceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            var cafeParams = new CafeParams
            {
                StartingWorkTime = 1,
                EndingWorkTime = 1
            };

            var pagingParams = new PagingParams();

            var controller = new CafesController(cafesServiceStub.Object, mapperStub.Object);
            var result = await controller.GetAll(cafeParams, pagingParams);

            result.Should().BeOfType<BadRequestObjectResult>();
            cafesServiceStub.Verify(x => x.GetAll(It.IsAny<CafeParams>(), It.IsAny<PagingParams>()), Times.Never);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        public async Task GetAll_WithEitherStartingOrEndingWorkTimeFiltered_CallsService(
            int? startingWorkTime, int? endingWorkTime)
        {
            var cafesServiceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            var cafeParams = new CafeParams
            {
                StartingWorkTime = startingWorkTime,
                EndingWorkTime = endingWorkTime
            };

            var pagingParams = new PagingParams();

            var controller = new CafesController(cafesServiceStub.Object, mapperStub.Object);
            var result = await controller.GetAll(cafeParams, pagingParams);

            cafesServiceStub.Verify(x => x.GetAll(It.IsAny<CafeParams>(), It.IsAny<PagingParams>()), Times.Once);
        }

        [Fact]
        public async Task GetById_WithValidParameters_ReturnsOkAndCafe()
        {
            string cafeId = new ObjectId().ToString();

            var serviceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            var returnedCafe = new Cafe
            {
                Id = cafeId,
                Name = "name"
            };

            var cafeViewModel = new CafeViewModel
            {
                Id = cafeId,
                Name = "name"
            };

            serviceStub.Setup(x => x.GetOne(cafeId))
                .ReturnsAsync(returnedCafe);

            mapperStub.Setup(x => x.Map<CafeViewModel>(returnedCafe))
                .Returns(cafeViewModel);

            var controller = new CafesController(serviceStub.Object, mapperStub.Object);

            var result = await controller.GetById(cafeId) as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(
                cafeViewModel,
                options => options.ComparingByMembers<CafeViewModel>());
        }

        [Fact]
        public async Task GetById_WithUnexistingCafe_ReturnsNotFound()
        {
            string cafeId = new ObjectId().ToString();

            var serviceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            Cafe returnedCafe = null;

            var controller = new CafesController(serviceStub.Object, mapperStub.Object);

            var result = await controller.GetById(cafeId);

            result.Should().BeOfType<NotFoundResult>();
            mapperStub.Verify(x => x.Map<CafeViewModel>(returnedCafe),
                Times.Never);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsBadRequest()
        {
            string cafeId = " wrong _ * id";

            var serviceStub = new Mock<ICafesService>();
            var mapperStub = new Mock<IMapper>();

            var controller = new CafesController(serviceStub.Object, mapperStub.Object);

            var result = await controller.GetById(cafeId);

            result.Should().BeOfType<BadRequestObjectResult>();
            mapperStub.Verify(x => x.Map<CafeViewModel>(It.IsAny<Cafe>()),
                Times.Never);
            serviceStub.Verify(x => x.GetOne(cafeId), Times.Never);
        }
    }
}
