using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T?> GetById(uint id, CancellationToken cancellationToken = default);
        Task<uint> Insert(T entity, CancellationToken cancellationToken = default);
        Task<bool> Update(T entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(uint id, CancellationToken cancellationToken = default);
    }
}
