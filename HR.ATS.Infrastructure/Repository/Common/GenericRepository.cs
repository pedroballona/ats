using System;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.Domain.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Infrastructure.Repository.Common
{
    public class GenericRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(nameof(T));
        }

        public Task<T> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _collection.AsQueryable().Where(e => e.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _collection.FindOneAndReplaceAsync(
                e => e.Id == entity.Id,
                entity,
                cancellationToken: cancellationToken
            );
            return entity;
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _collection.AsQueryable().Where(e => e.Id == id).AnyAsync(cancellationToken);
        }
    }
}