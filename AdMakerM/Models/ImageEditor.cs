using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using MetadataExtractor;

namespace AdMakerM
{
    class ImageEditor
    {
        static Random rnd = new Random();
        static string StatusString {get;set;}

        async internal static Task<BitmapImage> EditImage(string path, string newFileName)
        {
            int angle = 3;
            int contrast = 10;
            int brightness = 10;
            int saturation=10;//Изменяет насыщенность текущего изображения


            Bitmap bitmap = new Bitmap(path);
            int width = bitmap.Width;
            int height = bitmap.Height;

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

            
            double gipotenuseHorizontal = width;
            double gipotenuseVertical = height;
            
            double sin = Math.Sin(Math.PI * angle / 180); //n=0,5

            //высота в пикселях, которую нужно будет отдезать снизу и сверху
            double catetHorizontal = gipotenuseHorizontal * sin;
            double catetVertical = gipotenuseVertical * sin;


            byte[] photoBytes = File.ReadAllBytes(path);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 120 };
            Size size = new Size(randomWidth, resultHeight);

            BitmapImage bitmapNew;
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    IMatrixFilter matrixFilter = MatrixFilters.Gotham;
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(format)
                                    //.Filter(matrixFilter)
                                    .Rotate(angle)
                                    .Contrast(contrast)
                                    .Brightness(brightness)
                                    .Saturation(saturation);//Изменяет насыщенность текущего изображения

                        //обрезаем
                        int percentageHorizontal = (int)((catetHorizontal * 100) / imageFactory.Image.Height) + 1;
                        int percentageVertical = (int)((catetVertical * 100) / imageFactory.Image.Width) + 1;

                        
                        CropLayer cropLayer = new CropLayer(percentageVertical, percentageHorizontal, percentageVertical, percentageHorizontal);
                        cropLayer.CropMode = CropMode.Percentage;
                        imageFactory.Crop(cropLayer);

                        imageFactory.Save(outStream);

                    }
                    // Do something with the stream.
                    bitmapNew = new BitmapImage();
                    bitmapNew.BeginInit();
                    bitmapNew.StreamSource = outStream;
                    bitmapNew.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapNew.EndInit();
                    bitmapNew.Freeze();
                    bitmapNew.Save(newFileName);
                }
            }

            await Task.Delay(1);
            return bitmapNew;
        }

        async internal static Task CopyAndRotate(string path, string newCopyPath)
        {
            Bitmap img = new Bitmap(path);

            var directories = ImageMetadataReader.ReadMetadata(path);
            bool rotateToRight = false;
            foreach (var dir in directories)
            {
                if (dir.Name.Contains("Exif"))
                {
                    foreach (var tag in dir.Tags)
                    {

                        if (tag.Name == "Orientation")
                        {
                            Console.WriteLine(tag);
                            if (tag.Description.Contains("Rotate 90 CW"))
                            {
                                rotateToRight = true;
                            }
                        }
                    }
                }
            }

            byte[] photoBytes = File.ReadAllBytes(path);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 120 };

            BitmapImage bitmapNew;
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    IMatrixFilter matrixFilter = MatrixFilters.Gotham;
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Format(format);
                        if (rotateToRight) imageFactory.Rotate(90);


                        imageFactory.Save(outStream);

                    }

                    if (File.Exists(newCopyPath))
                    {
                        File.Delete(newCopyPath);
                    }

                    // Do something with the stream.
                    bitmapNew = new BitmapImage();
                    bitmapNew.BeginInit();
                    bitmapNew.StreamSource = outStream;
                    bitmapNew.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapNew.EndInit();
                    bitmapNew.Freeze();
                    bitmapNew.Save(newCopyPath);
                }
            }
            await Task.Delay(10);
        }

        async internal static Task<BitmapImage> EditImage(string path, string newFileName, int neededWith)
        {
            Bitmap img = new Bitmap(path);
            int width = img.Width;
            int height = img.Height;

            var directories = ImageMetadataReader.ReadMetadata(path);
            bool rotateToRight = false;
            foreach (var dir in directories)
            {
                if (dir.Name.Contains("Exif"))
                {
                    foreach (var tag in dir.Tags)
                    {

                        if (tag.Name == "Orientation")
                        {
                            Console.WriteLine(tag);
                            if (tag.Description.Contains("Rotate 90 CW"))
                            {
                                rotateToRight = true;
                            }
                        }
                    }
                }
            }

            int resultHeight = (neededWith * height) / width;

            StatusString = "Соотношение: " + (decimal)neededWith / (decimal)width;

            byte[] photoBytes = File.ReadAllBytes(path);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 120 };
            Size size = new Size(neededWith, resultHeight);

            BitmapImage bitmapNew;
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    IMatrixFilter matrixFilter = MatrixFilters.Gotham;
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Format(format);
                                    //.Filter(matrixFilter)
                                    //.Rotate(angle)
                                    //.Contrast(10)
                                    //.Brightness(10)
                                    //.Saturation(10);//Изменяет насыщенность текущего изображения
                        Console.WriteLine(imageFactory.Image.Width);
                        if (rotateToRight) imageFactory.Rotate(90);
                        Console.WriteLine(imageFactory.Image.Width);


                        imageFactory.Save(outStream);

                    }

                    if (File.Exists(newFileName))
                    {
                        File.Delete(newFileName);
                    }

                    // Do something with the stream.
                    bitmapNew = new BitmapImage();
                    bitmapNew.BeginInit();
                    bitmapNew.StreamSource = outStream;
                    bitmapNew.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapNew.EndInit();
                    bitmapNew.Freeze();
                    bitmapNew.Save(newFileName);
                }
            }
            //Bitmap resultImg = new Bitmap(img, new Size(neededWith, resultHeight));
            //if(File.Exists(newFileName))
            //{
            //    File.Delete(newFileName);
            //}

            //resultImg.Save(newFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            await Task.Delay(10);

            //BitmapImage source = BitmapToImageSource(img);
            return bitmapNew;
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

    public static class BitmapImageExtension
    {
        public static void Save(this BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
}
