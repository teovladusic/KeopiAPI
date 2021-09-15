using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Areas
{
    public class AreasService : IAreasService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<AreaDbModel> _repository;
        public AreasService(IMapper mapper, IGenericRepository<AreaDbModel> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public List<Area> GetByCityId(string cityId)
        {
            var areaDbModels = _repository.FilterBy(x => x.CityId == cityId);

            var areas = _mapper.Map<List<Area>>(areaDbModels);

            return areas;
        }
    }
}
