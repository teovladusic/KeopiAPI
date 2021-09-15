using AutoMapper;
using Keopi.Models;
using Keopi.Models.Params;
using Keopi.Service.Cafes;
using KeopiAPI.Models;
using KeopiDataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafesController : ControllerBase
    {
        private readonly ICafesService _cafesService;
        private readonly IMapper _mapper;
        public CafesController(ICafesService cafesService, IMapper mapper)
        {
            _cafesService = cafesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CafeParams cafeParams, [FromQuery] PagingParams pagingParams)
        {
            if (cafeParams.Seed < 1)
            {
                return BadRequest("Seed should be greater than 1");
            }

            if (cafeParams.StartingWorkTime != null && cafeParams.EndingWorkTime != null)
            {
                return BadRequest("You can filter either by starting or ending work time!");
            }

            var pagedCafes = await _cafesService.GetAll(cafeParams, pagingParams);

            var cafeViewModels = _mapper.Map<ListCafesViewModel>(pagedCafes);

            return Ok(cafeViewModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var objectId = new ObjectId(id);
            }
            catch
            {
                return BadRequest("Id specified is not valid!");
            }

            var cafe = await _cafesService.GetOne(id);

            if (cafe is null)
            {
                return NotFound();
            }

            var cafeViewModel = _mapper.Map<CafeViewModel>(cafe);

            return Ok(cafeViewModel);
        }
    }
}
