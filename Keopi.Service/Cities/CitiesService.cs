using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Cities
{
    public class CitiesService : ICitiesService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CityDbModel> _repository;
        public CitiesService(IMapper mapper, IGenericRepository<CityDbModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public List<City> GetAll()
        {
            var cityDbModels = _repository.AsQueryable().ToList();

            var cities = _mapper.Map<List<City>>(cityDbModels);

            return cities;
        }
    }
}
