using AutoMapper;
using Keopi.Service.Areas;
using KeopiAPI.Helpers;
using KeopiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeopiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAreasService _areasService;

        public AreasController(IMapper mapper, IAreasService areasService)
        {
            _mapper = mapper;
            _areasService = areasService;
        }

        [HttpGet]
        public IActionResult GetByCityId([RequiredFromQuery] string cityId)
        {
            if (cityId == null)
            {
                return BadRequest();
            }

            var areas = _areasService.GetByCityId(cityId);

            var areaViewModels = _mapper.Map<List<AreaViewModel>>(areas);

            return Ok(areaViewModels);
        } 
    }
}
