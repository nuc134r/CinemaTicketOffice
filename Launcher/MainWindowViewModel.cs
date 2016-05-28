using System;
using System.ComponentModel;
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
        private bool isTrustedConnection = true;
        private bool isConnected;

        public bool IsConnected
        {
            get { return isConnected; }
            private set { isConnected = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(MainWindow view)
        {
            this.view = view;
        }

        public string Server { get; set; }
        public string Database { get; set; }

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

                            var batchs = sql.Split(new[] {"GO"}, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var batch in batchs)
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
                    view.ShowDatabaseCreatedMessage();
                });
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
                }
                catch (Exception e)
                {
                    view.Dispatcher.Invoke(() => { view.IndicateFail(); });
                    IsConnected = false;
                }
            };

            connectWorker.RunWorkerAsync();
        }

        public void LaunchKiosk()
        {
            var kioskExecutable = currentDirectory + @"..\..\..\KioskClient\bin\Release\KioskClient.exe";
            Process.Start(kioskExecutable);
        }

        public void LaunchAdmin()
        {
            var adminExecutable = currentDirectory + @"..\..\..\Administration\bin\Release\Administration.exe";
            Process.Start(adminExecutable);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}