using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DataAccess
{
    public static class BitmapImageExtensions
    {
        public static byte[] ToByteArray(this BitmapImage imageSource)
        {
            var stream = imageSource.StreamSource;
            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (var reader = new BinaryReader(stream))
                {
                    buffer = reader.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }
    }
}