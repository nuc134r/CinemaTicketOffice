using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using DataAccess;
using DataAccess.Connection;

namespace Launcher
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private readonly MainWindow view;
        private bool isConnected;
        private bool isTrustedConnection = true;
        private bool existingDatabaseSelected = true;

        public MainWindowViewModel(MainWindow view)
        {
            this.view = view;
        }

        public bool IsConnected
        {
            get { return isConnected; }
            private set
            {
                isConnected = value;
                OnPropertyChanged();
            }
        }

        public string Server { get; set; }
        public string Database { get; set; }

        public bool ExistingDatabaseSelected
        {
            get { return existingDatabaseSelected; }
            set {
                existingDatabaseSelected = value;
                OnPropertyChanged();
                OnPropertyChanged("NewDatabaseSelected");
            }
        }

        public bool NewDatabaseSelected
        {
            get { return !existingDatabaseSelected; }
            set
            {
                existingDatabaseSelected = !value;
                OnPropertyChanged();
                OnPropertyChanged("ExistingDatabaseSelected");
            }
        }

        public bool IsTrustedConnection
        {
            get { return isTrustedConnection; }
            set
            {
                isTrustedConnection = value;
                OnPropertyChanged("IsNotTrustedConnection");
                if (value)
                {
                    User = "";
                    OnPropertyChanged("User");
                    Password = "";
                }
            }
        }

        public bool IsNotTrustedConnection
        {
            get { return !isTrustedConnection; }
        }

        public string User { get; set; }

        private string Password
        {
            get { return view.Password; }
            set { view.Password = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateDatabase()
        {
            var createDatabaseWorker = new BackgroundWorker();
            createDatabaseWorker.DoWork += (sender, args) =>
            {
                view.Dispatcher.Invoke(() => { view.ClearLog(); });
                view.Dispatcher.Invoke(() => { view.WriteLog("Соединение..."); });

                string connectionString;
                if (isTrustedConnection)
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master");
                }
                else
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master", User, Password);
                }

                try
                {
                    var sql = string.Format("CREATE DATABASE {0}", Database);
                    var executor = new CommandExecutor(sql, connectionString, false);
                    executor.ExecuteCommand(true).ThrowIfException();
                    view.Dispatcher.Invoke(() => { view.WriteLog("База данных создана: " + Database); });
                }
                catch (Exception e)
                {
                    view.Dispatcher.Invoke(() => { view.ShowError(e.Message); });
                    return;
                }

                if (isTrustedConnection)
                {
                    connectionString = ConnectionStringBuilder.Build(Server, Database);
                }
                else
                {
                    connectionString = ConnectionStringBuilder.Build(Server, Database, User, Password);
                }

                var directory = new DirectoryInfo(currentDirectory + @"..\..\..\Database");
                var files = directory.EnumerateFiles("?.*.sql");

                foreach (var fileInfo in files)
                {
                    using (var reader = new StreamReader(fileInfo.FullName))
                    {
                        try
                        {
                            var sql = reader.ReadToEnd();

                            var batches = sql.Split(new[] {"GO"}, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var batch in batches)
                            {
                                var executor = new CommandExecutor(batch, connectionString, false);
                                executor.ExecuteCommand(true).ThrowIfException();
                            }

                            view.Dispatcher.Invoke(() => { view.WriteLog("Выполнен скрипт: " + fileInfo.Name); });
                        }
                        catch (Exception e)
                        {
                            view.Dispatcher.Invoke(() => { view.ShowError(e.Message); });
                            return;
                        }
                    }
                }

                view.Dispatcher.Invoke(() =>
                {
                    view.WriteLog("Создание завершено");
                    view.OnDatabaseCreatedMessage();
                });

                if (isTrustedConnection)
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master");
                }
                else
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master", User, Password);
                }

                UpdateDatabasesList(connectionString);
            };

            createDatabaseWorker.RunWorkerAsync();
        }

        public void TryConnect()
        {
            var connectWorker = new BackgroundWorker();
            connectWorker.DoWork += (sender, args) =>
            {
                view.Dispatcher.Invoke(() => { view.IndicateProgress(); });

                string connectionString;
                if (isTrustedConnection)
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master");
                }
                else
                {
                    connectionString = ConnectionStringBuilder.Build(Server, "master", User, Password);
                }

                var executor = new CommandExecutor("SELECT GETDATE()", connectionString, false);

                try
                {
                    executor.ExecuteCommand().ThrowIfException();
                    view.Dispatcher.Invoke(() => { view.IndicateSuccess(); });
                    IsConnected = true;
                    UpdateDatabasesList(connectionString);
                }
                catch (Exception e)
                {
                    view.Dispatcher.Invoke(() => { view.IndicateFail(); });
                    IsConnected = false;
                }
            };

            connectWorker.RunWorkerAsync();
        }

        private void UpdateDatabasesList(string connectionString)
        {
            var cexecutor = new CommandExecutor("SELECT name FROM sys.databases d WHERE d.database_id > 4", connectionString, false);
            var result = cexecutor.ExecuteCommand().ThrowIfException();

            var databases = new List<string>();
            foreach (DataRow row in (result as DataSet).Tables[0].Rows)
            {
                databases.Add(row[0].ToString());
            }

            view.Dispatcher.Invoke(() => { view.SetDatabaseList(databases); });
        }

        public void LaunchKiosk()
        {
            var database = ExistingDatabaseSelected ? view.GetExisitingDatabaseName() : Database;

            var kioskExecutable = currentDirectory + @"..\..\..\KioskClient\bin\Release\KioskClient.exe";
            Process.Start(kioskExecutable, string.Format("-s {0} -d {1}", Server, database));
        }

        public void LaunchAdmin()
        {
            var database = ExistingDatabaseSelected ? view.GetExisitingDatabaseName() : Database;

            var adminExecutable = currentDirectory + @"..\..\..\Administration\bin\Release\Administration.exe";
            Process.Start(adminExecutable, string.Format("-s {0} -d {1}", Server, database));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}