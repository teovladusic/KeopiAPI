using Keopi.DataAccess.Models;
using Keopi.Models;
using KeopiDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Keopi.Models.Params;
using System.Collections;
using Keopi.Repository.Helpers;

namespace Keopi.Repository.Cafes
{
    public class CafesRepository : GenericRepository<CafeBarDbModel>, ICafesRepository
    {
        private readonly IMongoCollection<CafeBarDbModel> _collection;
        private readonly IRepositoryHelper _helper;
        public CafesRepository(IMongoClient mongoClient, IRepositoryHelper helper) : base(mongoClient)
        {
            var db = mongoClient.GetDatabase(DatabaseSettings.DatabaseName);
            _collection = db.GetCollection<CafeBarDbModel>(GetCollectionName(typeof(CafeBarDbModel)));
            _helper = helper;
        }

        public virtual async Task<PagedList<CafeBarDbModel>> FilterBy(CafeParams cafeParams, PagingParams pagingParams)
        {
            var pipeline = _helper.GetFiltersBuilder(cafeParams);

            PipelineDefinition<CafeBarDbModel, CafeBarDbModel> countPipe = pipeline.ToArray();
            var totalCount = _collection.Aggregate(countPipe).ToList().Count;

            var randomize = new BsonDocument("$addFields", new BsonDocument("remainderValue",
                new BsonDocument("$mod", new BsonArray { "$seed", cafeParams.Seed })));

            pipeline.Add(randomize);

            pipeline.Add(new BsonDocument("$sort", new BsonDocument("remainderValue", 1).Add("_id", 1)));

            pipeline.Add(new BsonDocument("$skip", (pagingParams.CurrentPage - 1) * pagingParams.PageSize));
            pipeline.Add(new BsonDocument("$limit", pagingParams.PageSize));

            PipelineDefinition<CafeBarDbModel, CafeBarDbModel> pipe = pipeline.ToArray();

            var asyncCursor = _collection.Aggregate(pipe);

            var list = await asyncCursor.ToListAsync();

            return new PagedList<CafeBarDbModel>(list, (int)totalCount, pagingParams.CurrentPage, pagingParams.PageSize);
        }
    }
}
