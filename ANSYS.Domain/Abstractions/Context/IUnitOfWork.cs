using System.Data;

namespace ANSYS.Domain.Abstractions.Context
{
    public interface IUnitOfWork
    {
        IDBContext Context { get; }
        IDbTransaction Transaction { get; }

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);
        bool CommitChanges();
        bool Rollback();
    }
}
