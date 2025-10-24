using ANSYS.Domain.Abstractions.Entities;
using ANSYS.Domain.Abstractions.Repositories;
using ANSYS.Infrastructure.Context;
using ANSYS.Infrastructure.Context.EntityFramework;
using ANSYS.Infrastructure.Context.Local;
using Microsoft.EntityFrameworkCore;

namespace ANSYS.Infrastructure.Abstractions.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        protected readonly AnsysEntityFrameworkContext _DBContext;
        protected static DBContextLocal<T> _dbLocal = new DBContextLocal<T>();

        public Repository(AnsysEntityFrameworkContext dbContext)
        {
            _DBContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
            {
                return _dbLocal.GetData();
            }
            else
            {
                var query = _DBContext.Set<T>();

                return await query.ToListAsync(cancellationToken);
            }
        }

        public async Task<T?> GetById(int id, CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
                return _dbLocal.GetById(id);
            else
                return await _DBContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);
        }

        public async Task<int> Insert(T entity, CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
            {
                _dbLocal.SetData(entity);
                return entity.Id;
            }

            var result = await _DBContext.Set<T>().AddAsync(entity, cancellationToken);

            return result.Entity.Id;//internal entry
        }

        public async Task<bool> Update(T entity, CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
            {
                return _dbLocal.UpdateData(entity);
            }

            _DBContext.Set<T>().Update(entity);
            await Task.CompletedTask;

            return true;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
                return _dbLocal.RemoveData(id);

            var entity = await GetById(id, cancellationToken);
            if (entity != null)
            {
                _DBContext.Set<T>().Remove(entity);
            }

            return true;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (DatabaseRunModeConfiguration.IsLocalDataBase)
                return 1;

            return await _DBContext.SaveChangesAsync(cancellationToken);
        }
    }
}
