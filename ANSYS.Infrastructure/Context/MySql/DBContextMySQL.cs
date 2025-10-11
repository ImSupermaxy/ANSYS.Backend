using ANSYS.Domain.Abstractions.Context;
using MySql.Data.MySqlClient;
using Npgsql;
using System.Data;

namespace ANSYS.Infrastructure.Context.MySql
{
    public sealed class DBContextMySQL : IDBContext, IDisposable
    {
        public IDbConnection Connection { get; private set; }
        public string ConnectionString { get; private set; }

        public DBContextMySQL(string conectString)
        {
            ConnectionString = conectString;
            CreateConnection();
        }

        public IDbConnection CreateConnection()
        {
            var connection = new MySqlConnection(ConnectionString);
            Connection = connection;
            Connection.Open();

            return Connection;
        }

        public bool CloseConnection()
        {
            if (Connection == null)
                return false;

            if (Connection.State != ConnectionState.Open)
                return false;

            Connection.Close();
            return true;
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
