using System;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using DataAccess.Connection;

namespace DataAccess.Repository
{
    public class SettingsRepository
    {
        private readonly string connectionString;

        public SettingsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public BitmapImage GetLogo()
        {
            var executor = new CommandExecutor("dbo.GetLogo", connectionString); 
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

            var dataSet = result as DataSet;
            
            var imageData = dataSet.Tables[0].Rows[0][0];
            if (imageData == DBNull.Value)
            {
                return null;
            }

            var posterBytes = (byte[]) imageData;
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(posterBytes);
            image.EndInit();

            return image;
        }

        public void SetLogo(BitmapImage logo)
        {
            var executor = new CommandExecutor("dbo.SetLogo", connectionString); 
            
            var logoData = logo == null ? DBNull.Value : (object)logo.ToByteArray();
            executor.AddParam("@Logo", logoData, SqlDbType.Image);

            executor.ExecuteCommand(true).ThrowIfException();
        }
    }
}