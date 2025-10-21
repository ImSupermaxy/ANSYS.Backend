using System.Data;

namespace ANSYS.Domain.Abstractions.Context.Connections
{
    public interface IUnitOfWork
    {
        IConnectionDBContext Context { get; }
        IDbTransaction Transaction { get; }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
        bool CommitChanges();
        bool Rollback();
    }
}
