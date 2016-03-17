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
        private readonly List<SqlParameter> paramList = new List<SqlParameter>();

        public CommandExecutor(string command, string connectionString, bool storedProc = true)
        {
            this.command = command;
            this.connectionString = connectionString;
            this.storedProc = storedProc;
        }

        public void AddParam(string name, object value, SqlDbType type, string typeName = null)
        {
            var param = new SqlParameter(name, type)
            {
                Value = value
            };

            if (typeName != null)
            {
                param.TypeName = typeName;
            }

            paramList.Add(param);
        }

        public object ExecuteCommand(bool nonQuery = false)
        {
            object result;
            var dataSet = new DataSet();

            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                var adapter = new SqlDataAdapter();
                var sqlCommand = new SqlCommand(command, connection)
                {
                    CommandType = storedProc ? CommandType.StoredProcedure : CommandType.Text
                };
                paramList.ForEach(param => sqlCommand.Parameters.Add(param));
                adapter.SelectCommand = sqlCommand;

                try
                {
                    connection.Open();
                    if (nonQuery)
                    {   
                        sqlCommand.ExecuteNonQuery();
                        result = null;
                    }
                    else
                    {
                        adapter.Fill(dataSet);
                        result = dataSet;
                    }
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
    }
}