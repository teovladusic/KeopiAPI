using Keopi.Service.Images;
using KeopiAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesService _imagesService;

        public ImagesController(IImagesService imagesService)
        {
            _imagesService = imagesService;
        }

        [HttpGet]
        public IActionResult GetByCafeId([RequiredFromQuery] string cafeId)
        {
            if (cafeId is null)
            {
                return BadRequest();
            }

            var images = _imagesService.GetByCafeId(cafeId);

            if (images.Length == 0)
            {
                return NotFound();
            }

            return Ok(images);
        }
    }
}
