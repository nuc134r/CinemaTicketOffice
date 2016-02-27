using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DataAccess.Connection
{
    public class CommandExecutor
    {
        private readonly string command;
        private readonly string connectionString;
        private readonly bool storedProc;
        private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

        public CommandExecutor(string command, string connectionString, bool storedProc = true)
        {
            this.command = command;
            this.connectionString = connectionString;
            this.storedProc = storedProc;
        }

        public object this[string index]
        {
            set { parameters.Add(index, value); }
        }

        public object ExecuteCommand()
        {
            object result;
            var dataSet = new DataSet();

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                var adapter = new SqlDataAdapter();
                var sqlCommand = new SqlCommand(command, connection);
                if (storedProc) { sqlCommand.CommandType = CommandType.StoredProcedure; }
                AddParametersTo(sqlCommand);
                adapter.SelectCommand = sqlCommand;

                try
                {
                    connection.Open();
                    adapter.Fill(dataSet);
                    result = dataSet;
                }
                catch (Exception ex)
                {
                    result = ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        private void AddParametersTo(SqlCommand sqlCommand)
        {
            if (parameters == null || parameters.Count == 0) return;
            foreach (var param in parameters)
            {
                var sqlParameter = new SqlParameter(param.Key, param.Value is int ? SqlDbType.Int : SqlDbType.NVarChar)
                {
                    Value = param.Value
                };
                sqlCommand.Parameters.Add(sqlParameter);
            }
        }
    }
}