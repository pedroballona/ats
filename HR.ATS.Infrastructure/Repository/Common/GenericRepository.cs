using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Infrastructure.Repository.Common
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        public GenericRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<T>(typeof(T).Name);
        }

        protected IMongoCollection<T> Collection { get; }

        public Task<T> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Collection.AsQueryable().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteOneAsync(d => d.Id == id, cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Collection.FindOneAndReplaceAsync(
                e => e.Id == entity.Id,
                entity,
                cancellationToken: cancellationToken
            );
            return entity;
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Collection.AsQueryable().Where(e => e.Id == id).AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Collection.AsQueryable().ToListAsync();
        }
    }
}