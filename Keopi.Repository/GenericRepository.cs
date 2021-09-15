using Keopi.DataAccess.Helpers;
using Keopi.DataAccess.Models;
using Keopi.Models;
using KeopiDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Keopi.Repository
{
    public class GenericRepository<TDocument> : IGenericRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        public GenericRepository(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(DatabaseSettings.DatabaseName);
            _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public virtual List<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            //this function should be called on objects that you know they don't need paging
            var result = _collection.Find(filterExpression).ToEnumerable();

            return result.ToList();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual async Task<List<TDocument>> Aggregate(
            PipelineDefinition<TDocument, TDocument> pipeline)
        {
            var result = await _collection.Aggregate(pipeline).ToListAsync();
            return result;
        }

        public virtual async Task<List<TProjected>> FilterBy<TProjected>(
            FilterDefinition<TDocument> filterDefinition,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            var result = _collection.Find(filterDefinition).Project(projectionExpression);
            return await result.ToListAsync();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}
