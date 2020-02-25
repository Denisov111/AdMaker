using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;

namespace AdMakerM
{
    class ImageEditor
    {
        static Random rnd = new Random();
        static string StatusString {get;set;}

        async internal static Task<BitmapImage> EditImage(string path, string newFileName)
        {
            Bitmap img = new Bitmap(path);
            int width = img.Width;
            int height = img.Height;

            
            int maxWidth = 0;
            int minWidth = 0;
            //если изображение меньше 1400, то его можно только увеличить, иначе - только уменьшить
            if (width < 1400)
            {
                //увеличиваем ширину в пределах 20%, но не менее чем на 3 пикселя
                maxWidth = (int)(width * 1.2);
                minWidth = width + 3;
                minWidth = (minWidth > maxWidth) ? maxWidth : minWidth;
            }
            else
            {
                //уменьшаем ширину в пределах 20%, но не менее чем на 3 пикселя
                maxWidth = 1350;
                minWidth = 800;
            }

            int randomWidth = rnd.Next(minWidth, maxWidth);
            int resultHeight = (randomWidth * height) / width;

            StatusString = "Соотношение: " + (decimal)randomWidth / (decimal)width;

            string fileName_ = Path.GetFileName(path).Replace(".jpg", "");

            Bitmap resultImg = new Bitmap(img, new Size(randomWidth, resultHeight));

            resultImg.Save(newFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            await Task.Delay(1);

            BitmapImage source = BitmapToImageSource(img);
            return source;
        }

        async internal static Task<BitmapImage> EditImage(string path, string newFileName, int neededWith)
        {
            Bitmap img = new Bitmap(path);
            int width = img.Width;
            int height = img.Height;

            int resultHeight = (neededWith * height) / width;

            StatusString = "Соотношение: " + (decimal)neededWith / (decimal)width;

            Bitmap resultImg = new Bitmap(img, new Size(neededWith, resultHeight));
            if(File.Exists(newFileName))
            {
                File.Delete(newFileName);
            }

            resultImg.Save(newFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            await Task.Delay(1);

            BitmapImage source = BitmapToImageSource(img);
            return source;
        }

        static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
