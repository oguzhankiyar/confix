using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OK.Confix.SqlServer.AdoNet.DataContexts
{
    internal class ConfixDataContext : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public ConfixDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public IList<T> QueryList<T>(Func<SqlDataReader, T> mapFunc, string commandText, object parameters = null) where T : new()
        {
            SqlDataReader reader = CreateCommand(commandText, parameters).ExecuteReader();

            IList<T> listOfObjects = new List<T>();

            while (reader.Read())
            {
                T obj = mapFunc(reader);

                listOfObjects.Add(obj);
            }

            return listOfObjects;
        }

        public T QueryFirst<T>(Func<SqlDataReader, T> mapFunc, string commandText, object parameters = null) where T : new()
        {
            SqlDataReader reader = CreateCommand(commandText, parameters).ExecuteReader();

            while (reader.Read())
            {
                T obj = mapFunc(reader);

                return obj;
            }

            return default(T);
        }
        
        public T ExecuteScalar<T>(string commandText, object parameters = null)
        {
            object result = CreateCommand(commandText, parameters).ExecuteScalar();

            return (T)result;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        #region Helpers

        private SqlCommand CreateCommand(string commandText, object parameters = null)
        {
            var command = Connection.CreateCommand();

            command.CommandType = CommandType.Text;
            command.CommandText = commandText;

            if (parameters != null)
            {
                foreach (var prop in parameters.GetType().GetProperties())
                {
                    command.Parameters.Add(new SqlParameter(prop.Name, prop.GetValue(parameters)));
                }
            }

            return command;
        }

        #endregion
    }
}