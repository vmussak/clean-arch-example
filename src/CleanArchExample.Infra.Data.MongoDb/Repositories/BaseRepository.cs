using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchExample.Infra.Data.MongoDb.Context;

namespace XPInc.WealthServices.Communication.Database.MongoDb.Repositories
{
    public abstract class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        protected readonly IMongoContext Context;
        protected IMongoCollection<TEntity> DatabaseCollection;

        protected BaseRepository(IMongoContext context, string collectionName)
        {
            Context = context;
            DatabaseCollection = Context.GetCollection<TEntity>(collectionName);
        }

        public virtual void Create(TEntity entity)
        {
            Context.AddCommand(() => DatabaseCollection.InsertOneAsync(entity));
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await DatabaseCollection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id)).ConfigureAwait(false);
            return data.SingleOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DatabaseCollection.FindAsync(Builders<TEntity>.Filter.Empty).ConfigureAwait(false);
            return all.ToList();
        }

        public virtual async Task<IList<TEntity>> GetByFilter(FilterDefinition<TEntity> filter)
        {
            var all = await DatabaseCollection.FindAsync(filter).ConfigureAwait(false);
            return all.ToList();
        }

        public virtual async Task<TEntity> GetFirst(FilterDefinition<TEntity> filter)
        {
            var all = await DatabaseCollection.FindAsync(filter, new FindOptions<TEntity, TEntity>
            {
                Limit = 1
            }).ConfigureAwait(false);

            return all?.FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirst(FilterDefinition<TEntity> filter, SortDefinition<TEntity> sort)
        {
            var all = await DatabaseCollection.FindAsync(filter, new FindOptions<TEntity, TEntity>
            {
                Limit = 1,
                Sort = sort
            }).ConfigureAwait(false);

            return all?.FirstOrDefault();
        }

        public virtual void Update(Guid id, TEntity entity)
        {
            Context.AddCommand(() => DatabaseCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity));
        }

        public virtual void Remove(Guid id)
        {
            Context.AddCommand(() => DatabaseCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
    }
}