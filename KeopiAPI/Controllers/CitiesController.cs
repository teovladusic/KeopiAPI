using AutoMapper;
using Keopi.Service.Cities;
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
    public class CitiesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICitiesService _cityService;
        public CitiesController(IMapper mapper, ICitiesService cityService)
        {
            _mapper = mapper;
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var cities = _cityService.GetAll();

            var cityViewModels = _mapper.Map<List<CityViewModel>>(cities);

            return Ok(cityViewModels);
        }
    }
}
