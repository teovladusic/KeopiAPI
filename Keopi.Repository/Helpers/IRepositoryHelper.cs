using Keopi.Models.Params;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Repository.Helpers
{
    public interface IRepositoryHelper
    {
        public List<BsonDocument> GetFiltersBuilder(CafeParams cafeParams);
    }
}
