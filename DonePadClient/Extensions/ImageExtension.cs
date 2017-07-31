using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DonePadClient.Extensions
{
    public static class ImageExtension
    {
        public static ImageSource Byte2Image(this byte[] imagebyte)
        {
            if (imagebyte == null)
                return null;
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(imagebyte);
            bmp.EndInit();
            return bmp;
        }
    }
}