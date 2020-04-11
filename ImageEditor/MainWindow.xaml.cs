using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.Specialized;
using System.Diagnostics;
using ImageProcessor;
using ImageProcessor.Common;
using ImageProcessor.Common.Exceptions;
using ImageProcessor.Common.Extensions;
using ImageProcessor.Configuration;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Colors;
using ImageProcessor.Imaging.Filters;
using ImageProcessor.Imaging.Filters.Artistic;
using ImageProcessor.Imaging.Filters.Binarization;
using ImageProcessor.Imaging.Filters.EdgeDetection;
using ImageProcessor.Imaging.Filters.Photo;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Imaging.Helpers;
using ImageProcessor.Imaging.MetaData;
using ImageProcessor.Imaging.Quantizers;
using ImageProcessor.Processors;
using System.Drawing;
using MetadataExtractor;

namespace ImageEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitmap = null;
        BitmapImage bitmapNew = null;
        BitmapImage bitmapCrop = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "All files (*.*)|*.*";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                

                string localPath = dialog.FileName;
                bitmap = new BitmapImage(new Uri(localPath));
                image1.Source = bitmap;

                var directories = ImageMetadataReader.ReadMetadata(localPath);
                bool rotateToRight = false;
                foreach(var dir in directories)
                {
                    if (dir.Name.Contains("Exif"))
                    {
                        foreach (var tag in dir.Tags)
                        {
                            
                            if(tag.Name=="Orientation")
                            {
                                Console.WriteLine(tag);
                                if(tag.Description.Contains("Rotate 90 CW"))
                                {
                                    rotateToRight = true;
                                }
                            }
                        }
                    }
                }

                double gipotenuseHorizontal = bitmap.Width;
                double gipotenuseVertical = bitmap.Height;
                int angle = -6;
                double sin = Math.Sin(Math.PI * angle / 180); //n=0,5

                //высота в пикселях, которую нужно будет отдезать снизу и сверху
                double catetHorizontal = Math.Abs(gipotenuseHorizontal * sin);
                double catetVertical = Math.Abs(gipotenuseVertical * sin);


                byte[] photoBytes = File.ReadAllBytes(localPath);
                // Format is automatically detected though can be changed.
                ISupportedImageFormat format = new JpegFormat { Quality = 120 };
                System.Drawing.Size size = new System.Drawing.Size(1500, 0);

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
                                        //.Resize(size)
                                        .Format(format)
                                        //.Filter(matrixFilter)
                                        .Rotate(angle)
                                        .Contrast(10)
                                        .Brightness(10)
                                        .Saturation(10);//Изменяет насыщенность текущего изображения
                            if (rotateToRight) imageFactory.Rotate(90);
                            imageFactory.Save(outStream);
                            
                        }
                        // Do something with the stream.
                        bitmapNew = new BitmapImage();
                        bitmapNew.BeginInit();
                        bitmapNew.StreamSource = outStream;
                        bitmapNew.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapNew.EndInit();
                        bitmapNew.Freeze();

                        image2.Source = bitmapNew;
                        bitmapNew.Save("5sdrgsegr.jpg");

                        
                    }

                    int percentageHorizontal = (int)((catetHorizontal * 100) / bitmapNew.Height)+2;
                    int percentageVertical = (int)((catetVertical * 100) / bitmapNew.Width)+2;

                    using (MemoryStream outStream = new MemoryStream())
                    {
                        CropLayer cropLayer = new CropLayer(percentageVertical, percentageHorizontal, percentageVertical, percentageHorizontal);
                        cropLayer.CropMode = CropMode.Percentage;
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load("5sdrgsegr.jpg")
                                        //.Resize(size)
                                        .Crop(cropLayer)
                                        .Save(outStream);
                        }

                        // Do something with the stream.
                        bitmapCrop = new BitmapImage();
                        bitmapCrop.BeginInit();
                        bitmapCrop.StreamSource = outStream;
                        bitmapCrop.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapCrop.EndInit();
                        bitmapCrop.Freeze();

                        image3.Source = bitmapCrop;
                        bitmapCrop.Save("crop_5sdrgsegr.jpg");

                    }
                }

            }
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TransformedBitmap TempImage = new TransformedBitmap();
            TempImage.BeginInit();
            TempImage.Source = bitmap; // MyImageSource of type BitmapImage

            RotateTransform transform = new RotateTransform(12);
            TempImage.Transform = transform;
            TempImage.EndInit();

            image1.Source = TempImage;
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
