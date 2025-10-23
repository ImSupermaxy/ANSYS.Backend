using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Repositories
{
    public interface IRepository<T, TId>
        where T : Entity<TId>
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T?> GetById(TId id, CancellationToken cancellationToken = default);
        Task<TId> Insert(T entity, CancellationToken cancellationToken = default);
        Task<bool> Update(T entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(TId id, CancellationToken cancellationToken = default);
    }
}
