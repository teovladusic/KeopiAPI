using Keopi.Models;
using Keopi.Models.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Service.Cafes
{
    public interface ICafesService
    {
        public Task<PagedList<Cafe>> GetAll(CafeParams cafeParams, PagingParams pagingParams);

        public Task<Cafe> GetOne(string id);
    }
}
