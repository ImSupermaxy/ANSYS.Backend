using ANSYS.Domain.Abstractions.Context.Connections;
using System.Data;

namespace ANSYS.Infrastructure.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConnectionDBContext Context { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IConnectionDBContext context)
        {
            Context = context;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            if (Transaction == null)
            {
                Transaction = Context.Connection.BeginTransaction(isolationLevel);
            }
            //else
            //{
            //    throw new OverflowException();
            //}
        }

        public bool CommitChanges()
        {
            try
            {
                Transaction?.Commit();
                DefaultOperation();

                return true;
            }
            catch (Exception ex)
            {
                DefaultOperation();
                return false;
            }
        }

        public bool Rollback()
        {
            try
            {
                Transaction?.Rollback();
                DefaultOperation();

                return true;
            }
            catch (Exception ex)
            {
                DefaultOperation();
                return false;
            }
        }

        private void DefaultOperation()
        {
            Transaction?.Dispose();
            Transaction = null;
        }
    }
}
