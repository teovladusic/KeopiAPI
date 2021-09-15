using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.PromoCafes
{
    public class PromoCafesService : IPromoCafesService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<PromoCafeBarDbModel> _promoCafesRepository;
        private readonly IGenericRepository<CafeBarDbModel> _cafesRepository;

        public PromoCafesService(IMapper mapper, IGenericRepository<PromoCafeBarDbModel> promoCafesRepository,
            IGenericRepository<CafeBarDbModel> cafesRepository)
        {
            _mapper = mapper;
            _promoCafesRepository = promoCafesRepository;
            _cafesRepository = cafesRepository;
        }

        public async Task<List<Cafe>> GetAll()
        {
            var promoCafeDbModels = _promoCafesRepository.AsQueryable();

            var promoCafes = _mapper.Map<List<PromoCafe>>(promoCafeDbModels.ToList());

            var cafeDbModels = new List<CafeBarDbModel>();

            foreach (var promoCafe in promoCafes)
            {
                var cafe = await _cafesRepository.FindByIdAsync(promoCafe.CafeId);
                cafeDbModels.Add(cafe);
            }

            var cafes = _mapper.Map<List<Cafe>>(cafeDbModels);

            return cafes;
        }
    }
}
