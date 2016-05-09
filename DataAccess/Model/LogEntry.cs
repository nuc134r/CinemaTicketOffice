using System;

namespace DataAccess.Model
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string TableName { get; set; }
        public int? EntitiyId { get; set; }
        public OperationType OperationType { get; set; }

        public string OperationTypeString
        {
            get
            {
                switch (OperationType)
                {
                    case OperationType.Creation:
                        return Resources.CreationText;
                    case OperationType.Editing:
                        return Resources.EditingText;
                    case OperationType.Deletion:
                        return Resources.DeletionText;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public LogEntry Clone()
        {
            return new LogEntry
            {
                Id = Id, Date = Date, User = User, OperationType = OperationType, EntitiyId = EntitiyId, TableName = TableName
            };
        }
    }
}