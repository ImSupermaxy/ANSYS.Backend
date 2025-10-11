using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Mappers
{
    public interface IEntityMapper<T, TCommandInsert, TCommandUpdate, TCommandQuery> : IMapper
        where T : IEntity
        where TCommandInsert : ICommand
        where TCommandUpdate : ICommand
        where TCommandQuery : ICommand
    {
        T ToEntity(TCommandInsert command);
        T ToEntity(TCommandUpdate command);
        T ToEntity(TCommandQuery command);
        TCommandQuery ToQuery(T entity);
    }
}
