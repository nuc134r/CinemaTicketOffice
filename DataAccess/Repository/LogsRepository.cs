using System.Collections.Generic;
using System.Data;
using DataAccess.Connection;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class LogsRepository
    {
        private readonly string connectionString;

        public LogsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<LogEntry> GetLogEntries()
        {
            var executor = new CommandExecutor("dbo.BrowseLogs", connectionString);
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new LogEntry
                {
                    Id = row["Id"].ToInt(),
                    Date = row["Date"].ToDate(),
                    User = row["User"].ToString(),
                    OperationType = (OperationType) (row["OperationType"].ToInt() + 1),
                    EntitiyId = row["EntityId"].ToNulllableInt(),
                    TableName = row["EntityTable"].ToString()
                };
            }
        }
    }
}