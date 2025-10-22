using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> Insert(T entity, CancellationToken cancellationToken = default);
        Task<bool> Update(T entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
