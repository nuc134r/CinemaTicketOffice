using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace DataAccess
{
    public static class BitmapImageExtensions
    {
        public static byte[] ToByteArray(this BitmapImage image)
        {
            return File.ReadAllBytes(image.UriSource.AbsolutePath);
        }
    }
}