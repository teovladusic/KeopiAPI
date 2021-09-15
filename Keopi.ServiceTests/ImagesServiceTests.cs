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
using MongoDB.Bson;
using Keopi.Service.Images;

namespace Keopi.ServiceTests
{
    public class ImagesServiceTests
    {
        [Fact]
        public void GetByCafeId_WithValidCafeId_RetursImages()
        {
            var repositoryStub = new Mock<IGenericRepository<ImagesDbModel>>();

            string[] images = { "image1", "image2" };

            var cafeId = ObjectId.GenerateNewId().ToString();

            var imageDbModels = new List<ImagesDbModel>
            {
                new ImagesDbModel
                {
                    Id = ObjectId.GenerateNewId(),
                    CafeId = cafeId,
                    Urls = images
                }
            };

            repositoryStub.Setup(x => x.FilterBy(y => y.CafeId == cafeId))
                .Returns(imageDbModels);

            var service = new ImagesService(repositoryStub.Object);

            var result = service.GetByCafeId(cafeId);

            result.Should().BeEquivalentTo(
                images,
                options => options.ComparingByMembers<string>());
        }
    }
}
