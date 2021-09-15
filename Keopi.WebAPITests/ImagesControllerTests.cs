using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Keopi.Service.Images;
using MongoDB.Bson;
using KeopiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Keopi.WebAPITests
{
    public class ImagesControllerTests
    {
        [Fact]
        public void GetByCafeId_WithValidId_ReturnsOkAndUrls()
        {
            var serviceStub = new Mock<IImagesService>();

            string[] images = { "image1", "image2" };

            string cafeId = ObjectId.GenerateNewId().ToString();

            serviceStub.Setup(x => x.GetByCafeId(cafeId))
                .Returns(images);

            var controller = new ImagesController(serviceStub.Object);

            var result = controller.GetByCafeId(cafeId) as OkObjectResult;

            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(
                images,
                options => options.ComparingByMembers<string>());
        }

        [Fact]
        public void GetByCafeId_WithInvalidId_ReturnsBadRequest()
        {
            var serviceStub = new Mock<IImagesService>();

            string cafeId = null;

            var controller = new ImagesController(serviceStub.Object);

            var result = controller.GetByCafeId(cafeId);

            result.Should().BeOfType<BadRequestResult>();
            serviceStub.Verify(x => x.GetByCafeId(null), Times.Never);
        }

        [Fact]
        public void GetByCafeId_WithUnexistingCafe_ReturnsNotFound()
        {
            var serviceStub = new Mock<IImagesService>();

            string[] images = Array.Empty<string>();

            string cafeId = ObjectId.GenerateNewId().ToString();

            serviceStub.Setup(x => x.GetByCafeId(cafeId))
                .Returns(images);

            var controller = new ImagesController(serviceStub.Object);

            var result = controller.GetByCafeId(cafeId);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
