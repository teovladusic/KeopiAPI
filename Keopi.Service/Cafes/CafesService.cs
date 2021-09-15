using AutoMapper;
using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Models.Params;
using Keopi.Repository;
using Keopi.Repository.Cafes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Cafes
{
    public class CafesService : ICafesService
    {
        private readonly ICafesRepository _cafesRepository;
        private readonly IMapper _mapper;
        public CafesService(ICafesRepository cafesRepository, IMapper mapper)
        {
            _cafesRepository = cafesRepository;
            _mapper = mapper;
        }

        public async Task<PagedList<Cafe>> GetAll(CafeParams cafeParams, PagingParams pagingParams)
        {
            var pagedCafesDbModels = await _cafesRepository.FilterBy(cafeParams, pagingParams);

            var cafes = _mapper.Map<List<Cafe>>(pagedCafesDbModels);

            var pagedCafes = new PagedList<Cafe>(
                cafes,
                pagedCafesDbModels.TotalCount,
                pagedCafesDbModels.CurrentPage,
                pagedCafesDbModels.PageSize);

            return pagedCafes;
        }
        
        public async Task<Cafe> GetOne(string id)
        {
            var cafeDbModel = await _cafesRepository.FindByIdAsync(id);

            var cafe = _mapper.Map<Cafe>(cafeDbModel);

            return cafe;
        }
    }
}
