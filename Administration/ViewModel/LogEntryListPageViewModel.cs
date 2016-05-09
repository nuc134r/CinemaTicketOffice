using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Editors;
using Administration.Properties;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class LogEntryListPageViewModel
    {
        private readonly LogsRepository repository;
        private readonly LogEntryListPage view;

        public LogEntryListPageViewModel(LogEntryListPage view, LogsRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveData();
        }

        public ObservableCollection<LogEntry> LogEntryList { get; private set; }

        private void RetrieveData()
        {
            if (LogEntryList == null)
            {
                LogEntryList = new ObservableCollection<LogEntry>();
                LogEntryList.CollectionChanged += TicketsOnCollectionChanged;
            }

            var logEntries = repository.GetLogEntries().ToList();

            LogEntryList.Clear();
            logEntries.ForEach(ticket => LogEntryList.Add(ticket));
        }

        private void TicketsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            view.ListCount = LogEntryList.Count;
        }

        public void OpenEditor(LogEntry logEntry)
        {
            try
            {
                if (logEntry != null)
                {
                    logEntry = logEntry.Clone();
                }

                var editor = new LogEntryEditorWindow(logEntry);
                var result = editor.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    RetrieveData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}