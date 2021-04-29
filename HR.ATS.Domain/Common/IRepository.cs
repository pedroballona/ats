using System;
using System.Threading;
using System.Threading.Tasks;

namespace HR.ATS.Domain.Common
{
    public interface IRepository<T> where T : Entity
    {
        public Task<T> FindAsync(Guid id, CancellationToken cancellationToken = default);
        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}