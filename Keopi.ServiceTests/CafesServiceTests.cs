using AutoMapper;
using FluentAssertions;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Models.Params;
using Keopi.Repository;
using Keopi.Repository.Cafes;
using Keopi.Service.Cafes;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Keopi.ServiceTests
{
    public class CafesServiceTests
    {
        [Fact]
        public async Task GetAll_WithValidParams_ReturnsCafes()
        {
            var cafeParams = new CafeParams
            {
                Seed = 123,
            };

            var pagingParams = new PagingParams
            {
                PageSize = 5
            };

            var models = new List<CafeBarDbModel> { new CafeBarDbModel { Name = "name" } };
            var pagedModelsToReturn = new PagedList<CafeBarDbModel>(models, models.Count, pagingParams.CurrentPage, 
                pagingParams.PageSize);

            var pagedModels = new List<Cafe> { new Cafe { Name = "name" } };
            var pagedMappedModels = new PagedList<Cafe>(pagedModels, pagedModels.Count, pagingParams.CurrentPage,
                pagingParams.PageSize);

            var repositoryStub = new Mock<ICafesRepository>();
            repositoryStub.Setup(x => x.FilterBy(cafeParams, pagingParams))
                .ReturnsAsync(pagedModelsToReturn);

            var mapperStub = new Mock<IMapper>();
            mapperStub.Setup(x => x.Map<List<Cafe>>(pagedModelsToReturn))
                .Returns(pagedMappedModels);

            var service = new CafesService(repositoryStub.Object, mapperStub.Object);

            var result = await service.GetAll(cafeParams, pagingParams);

            result.Should().BeEquivalentTo(
                pagedMappedModels,
                options => options.ComparingByMembers<Cafe>());
        }

        [Fact]
        public async Task GetOne_WithValidParams_ReturnsCafe()
        {
            var repositoryStub = new Mock<ICafesRepository>();

            var id = new ObjectId();

            var dbModelToReturn = new CafeBarDbModel
            {
                Id = id,
                Name = "name"
            };

            repositoryStub.Setup(x => x.FindByIdAsync(id.ToString()))
                .ReturnsAsync(dbModelToReturn);

            var mapperStub = new Mock<IMapper>();

            var mappedCafe = new Cafe
            {
                Id = id.ToString(),
                Name = "name"
            };

            mapperStub.Setup(x => x.Map<Cafe>(dbModelToReturn))
                .Returns(mappedCafe);

            var service = new CafesService(repositoryStub.Object, mapperStub.Object);

            var result = await service.GetOne(id.ToString());

            result.Should().BeEquivalentTo(
                mappedCafe,
                options => options.ComparingByMembers<Cafe>());
        }
    }
}
