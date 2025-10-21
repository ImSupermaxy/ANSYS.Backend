using System.Data;

namespace ANSYS.Domain.Abstractions.Context.Connections
{
    public interface IConnectionDBContext : IDisposable
    {
        string ConnectionString { get; }
        IDbConnection Connection { get; }
        IDbConnection CreateConnection();
        bool CloseConnection();
    }
}