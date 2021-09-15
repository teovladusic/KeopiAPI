using AutoMapper;
using FluentAssertions;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Repository;
using Keopi.Service.PromoCafes;
using MongoDB.Bson;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Keopi.ServiceTests
{
    public class PromoCafesServiceTests
    {
        [Fact]
        public async Task GetAll_WithValidParameters_ReturnsCafes()
        {
            var promoCafesRepositoryStub = new Mock<IGenericRepository<PromoCafeBarDbModel>>();
            var cafesRepositoryStub = new Mock<IGenericRepository<CafeBarDbModel>>();
            var mapperStub = new Mock<IMapper>();

            var cafe1ObjectId = ObjectId.GenerateNewId();
            var cafe2ObjectId = ObjectId.GenerateNewId();

            var promoCafeDbModelsToReturn = new List<PromoCafeBarDbModel>
            {
                new PromoCafeBarDbModel { Id = ObjectId.GenerateNewId(), CafeId = cafe1ObjectId.ToString() },
                new PromoCafeBarDbModel { Id = ObjectId.GenerateNewId(), CafeId = cafe1ObjectId.ToString() }
            };

            promoCafesRepositoryStub.Setup(x => x.AsQueryable())
                .Returns(promoCafeDbModelsToReturn.AsQueryable());

            var promoCafes = new List<PromoCafe>
            {
                new PromoCafe { Id = promoCafeDbModelsToReturn[0].Id.ToString(), CafeId = cafe1ObjectId.ToString() },
                new PromoCafe { Id = promoCafeDbModelsToReturn[1].Id.ToString(), CafeId = cafe2ObjectId.ToString() }
            };

            mapperStub.Setup(x => x.Map<List<PromoCafe>>(promoCafeDbModelsToReturn))
                .Returns(promoCafes);

            var cafeBarDbModel1 = new CafeBarDbModel { Id = cafe1ObjectId };
            var cafeBarDbModel2 = new CafeBarDbModel { Id = cafe2ObjectId };

            cafesRepositoryStub.Setup(x => x.FindByIdAsync(cafe1ObjectId.ToString()))
                .ReturnsAsync(cafeBarDbModel1);

            cafesRepositoryStub.Setup(x => x.FindByIdAsync(cafe2ObjectId.ToString()))
            .ReturnsAsync(cafeBarDbModel2);

            var cafe1 = new Cafe { Id = cafe1ObjectId.ToString() };
            var cafe2 = new Cafe { Id = cafe2ObjectId.ToString() };

            var listOfCafeDbModels = new List<CafeBarDbModel> { cafeBarDbModel1, cafeBarDbModel2 };

            var list = new List<Cafe> { cafe1, cafe2 };

            mapperStub.Setup(x => x.Map<List<Cafe>>(listOfCafeDbModels))
                .Returns(list);

            var service = new PromoCafesService(mapperStub.Object, promoCafesRepositoryStub.Object,
                cafesRepositoryStub.Object);

            var result = await service.GetAll();

            result.Should().BeEquivalentTo(
                list,
                options => options.ComparingByMembers<Cafe>());
        }
    }
}
