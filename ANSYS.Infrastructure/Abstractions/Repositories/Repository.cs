using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Abstractions.Repositories;
using ANSYS.Infrastructure.Context.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ANSYS.Infrastructure.Abstractions.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly AnsysEntityFrameworkContext _DBContext;

        public Repository(AnsysEntityFrameworkContext dbContext)
        {
            _DBContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            var query = _DBContext.Set<T>();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetById(uint id, CancellationToken cancellationToken = default)
        {
            return await _DBContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
        }

        public async Task<uint> Insert(T entity, CancellationToken cancellationToken = default)
        {
            var result = await _DBContext.Set<T>().AddAsync(entity, cancellationToken);

            return result.Entity.Id;
        }

        public async Task<bool> Update(T entity, CancellationToken cancellationToken = default)
        {
            _DBContext.Set<T>().Update(entity);
            await Task.CompletedTask;

            return true;
        }

        public async Task<bool> Delete(uint id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity != null)
            {
                _DBContext.Set<T>().Remove(entity);
            }

            return true;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _DBContext.SaveChangesAsync(cancellationToken);
        }
    }
}
