using DataAccess.Model;

namespace Administration.Editors
{
    public partial class LogEntryEditorWindow
    {
        public LogEntryEditorWindow(LogEntry logEntry)
        {
            InitializeComponent();

            DataContext = logEntry;
        }
    }
}