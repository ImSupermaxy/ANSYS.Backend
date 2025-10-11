using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Repositories
{
    public interface IRepository<T, TCommandQuery>
        where T : IEntity
        where TCommandQuery : ICommand
    {
        Task<IEnumerable<TCommandQuery>> GetAll();
        Task<TCommandQuery> GetById(uint id);
        Task<uint> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(uint id);
    }
}
