using System.Data;

namespace ANSYS.Domain.Abstractions.Context
{
    public interface IDBContext : IDisposable
    {
        string ConnectionString { get; }
        IDbConnection Connection { get; }
        IDbConnection CreateConnection();
        bool CloseConnection();
    }
}