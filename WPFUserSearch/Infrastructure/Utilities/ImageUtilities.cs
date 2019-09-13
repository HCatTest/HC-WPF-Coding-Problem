using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFUserSearch.Infrastructure.Utilities
{
    public class ImageUtilities
    {
        public static byte[] ConvertBitmapSourceToByteArray(string filepath)
        {
            byte[] data = null;

            try
            {
                var image = new BitmapImage(new Uri(filepath));
                
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
            }
            catch(Exception ex)
            {
            }

            return data;
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(Byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;

            var stream = new MemoryStream(bytes);
            stream.Seek(0, SeekOrigin.Begin);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            return image;
        }
    }
}