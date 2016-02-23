using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CommandExecutor
    {
        private readonly string command;
        private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

        public CommandExecutor(string command)
        {
            this.command = command;
        }

        public object this[string index]
        {
            set { parameters.Add(index, value); }
        }

        public DataSet ExecuteCommand()
        {
            var dataSet = new DataSet();

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                var adapter = new SqlDataAdapter();
                var sqlCommand = new SqlCommand(command, connection) {CommandType = CommandType.StoredProcedure};
                AddParametersTo(sqlCommand);
                adapter.SelectCommand = sqlCommand;

                connection.Open();
                adapter.Fill(dataSet);
                
                connection.Close();
            }

            return dataSet;
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