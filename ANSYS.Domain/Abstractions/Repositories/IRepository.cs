using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Repositories
{
    public interface IRepository<T>
        where T : Entity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T?> GetById(int id, CancellationToken cancellationToken = default);
        Task<int> Insert(T entity, CancellationToken cancellationToken = default);
        Task<bool> Update(T entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
