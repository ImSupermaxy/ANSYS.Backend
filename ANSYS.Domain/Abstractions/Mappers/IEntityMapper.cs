using ANSYS.Domain.Abstractions.Commands;
using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Domain.Abstractions.Mappers
{
    public interface IEntityMapper<T, TCommandInsert, TCommandUpdate> : IMapper
        where T : IEntity
        where TCommandInsert : ICommand
        where TCommandUpdate : ICommand
    {
        T ToEntity(TCommandInsert command);
        T ToEntity(TCommandUpdate command);
    }
}
