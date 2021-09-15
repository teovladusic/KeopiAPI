using AutoMapper;
using Keopi.Service.PromoCafes;
using KeopiAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keopi.Models;

namespace KeopiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCafesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPromoCafesService _promoCafesService;
        public PromoCafesController(IMapper mapper, IPromoCafesService promoCafesService)
        {
            _mapper = mapper;
            _promoCafesService = promoCafesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promoCafes = await _promoCafesService.GetAll();

            var cafeViewModels = _mapper.Map<List<CafeViewModel>>(promoCafes);

            return Ok(cafeViewModels);
        }
    }
}
