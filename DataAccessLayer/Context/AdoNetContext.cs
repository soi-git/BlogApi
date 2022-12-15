using Microsoft.Data.Sqlite;

namespace DataAccessLayer.Context
{
    public class AdoNetContext : IDisposable
    {
        private SqliteConnection? _connection;
        private bool _ownsConnection;
        private SqliteTransaction? _transaction;

        public AdoNetContext(string connectionString, bool ownsConnection)
        {
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
            _ownsConnection = ownsConnection;
            _transaction = _connection.BeginTransaction();
        }
        public SqliteCommand CreateCommand()
        {
            var command = _connection?.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }
        public void SaveChanges()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Отсутствует не завершенная транзакция.");
            }
            _transaction.Commit();
            _transaction = null;
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (_connection != null && _ownsConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
