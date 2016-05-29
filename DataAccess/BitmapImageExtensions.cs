using System.IO;
using System.Windows.Media.Imaging;

namespace DataAccess
{
    public static class BitmapImageExtensions
    {
        public static byte[] ToByteArray(this BitmapImage image)
        {
            if (image.StreamSource == null)
            {
                return File.ReadAllBytes(image.UriSource.AbsolutePath);
            }

            var stream = image.StreamSource;
            byte[] buffer = null;
            if (stream != null && stream.Length > 0)
            {
                using (var reader = new BinaryReader(stream))
                {
                    stream.Position = 0;
                    buffer = reader.ReadBytes((int) stream.Length);
                }
            }

            return buffer;
        }
    }
}