using Keopi.DataAccess.Models;
using Keopi.Models;
using Keopi.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Repository.Cafes
{
    public interface ICafesRepository : IGenericRepository<CafeBarDbModel>
    {
        public Task<PagedList<CafeBarDbModel>> FilterBy(CafeParams cafeParams, PagingParams pagingParams);
    }
}
