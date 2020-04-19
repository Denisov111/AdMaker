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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;
using System.Net;

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для AdCard.xaml
    /// </summary>
    public partial class AdCard : Window
    {
        public Ad Ad { get; set; }
        Global gl;
        public ObservableCollection<IProduct> Products { get; set; } = new ObservableCollection<IProduct>();
        public ObservableCollection<Product> Parts { get; set; } = new ObservableCollection<Product>();
        public decimal Price { get; set; } = 0;

        public AdCard(Ad ad, Global gl)
        {
            InitializeComponent();
            this.gl = gl;
            Ad = ad;
            if(ad.Articul==0)
            {
                MessageBox.Show("Забыли сохранить объявления");
                Close(); 
            }
            DataContext = this;

            SetProducts();

            if (Ad.ModImgFileName == null) Button_Click_5(null,null);
        }

        private void SetProducts()
        {
            foreach(string guid in Ad.Guids)
            {
                IProduct pr = GetProductByGuid(guid);
                if(pr!=null)
                {
                    Products.Add(pr);
                    Price += pr.Price;
                }
                    
            }
        }

        private IProduct GetProductByGuid(string guid)
        {
            IProduct pr = null;

            pr = gl.CaseOptions.Where(p=>p.Guid==guid).FirstOrDefault();
            if(pr!=null) return pr;
            pr = gl.CPUCoolerOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.HDDOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.MBOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.MemoryOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.PowerOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.ProcessorOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.SSDOptions.Where(p => p.Guid == guid).FirstOrDefault();
            if (pr != null) return pr;
            pr = gl.VideoAdapters.Where(p => p.Guid == guid).FirstOrDefault();

            return pr;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Ad.ModImgFileName);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string tag = ((FrameworkElement)sender).Tag?.ToString();
            if (tag == null) return;
            System.Diagnostics.Process.Start(tag);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Ad.Title);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Ad.Description);
        }

        async private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        async private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            if (!Directory.Exists("modimages"))
            {
                Directory.CreateDirectory("modimages");
            }
            string imagePath = Ad.ImgFileName;
            string newFileName = "modimages\\" + Ad.Articul.ToString() + ".jpg";
            string newPath = System.IO.Path.Combine(currentDir, newFileName);
            if (!String.IsNullOrWhiteSpace(imagePath))
            {
                try
                {
                    await ImageEditor.EditImage(imagePath, newPath);

                    Ad.ModImgFileName = newPath;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            gl.SaveAll();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PubOnYoula();
            gl.AdsArchive.Add(Ad);
            gl.SaveAll();
        }

        private void PubOnYoula()
        {
            
            string path = UploadImage(Ad.ModImgFileName);

            XDocument xmlData = new XDocument(new XElement("ads"));
            string offerId = Ad.Articul.ToString();
            string youlaCategoryId = "15";
            string youlaSubcategoryId = "1502";
            string name = Ad.Title;
            string picture = "http://youla.ria.su/images/"+ path;
            string price = Ad.BuyPrice.ToString();
            string description = Ad.Description;

            if(Ad.BuyPrice < Ad.Price)
            {
                MessageBoxResult result = MessageBox.Show("Цена не может быть ниже себестоимости, продолжить с ценой ниже себестоимости", "My App", MessageBoxButton.YesNoCancel);

                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            XElement xmlAd = new XElement("ad",
                new XElement("offer_id", offerId),
                new XElement("price", price),
                new XElement("youla_category_id", youlaCategoryId),
                new XElement("youla_subcategory_id", youlaSubcategoryId),
                new XElement("imglink", picture),
                new XElement("name", name),
                new XElement("description", description));
            xmlData.Root.Add(xmlAd);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://youla.ria.su/xml_rpc_reader.php");
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://youla.ria.su");
            request.Method = "POST";

            // XML-RPC-команда
            string command = xmlData.ToString();

            byte[] bytes = Encoding.UTF8.GetBytes(command);
            request.ContentLength = bytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var stream = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                // Вывод ответа от сервера на консоль.
                string resp = stream.ReadToEnd();
                Console.WriteLine(resp);
            }
            Ad.IsPostedOnUla = true;
        }

        internal static string UploadImage(string fileName)
        {
            string imgFileName = "";
            try
            {
                imgFileName = System.IO.Path.GetFileName(fileName);
                //Отправляем файл архива
                WebClient myWebClient = new WebClient();
                byte[] responseArray = myWebClient.UploadFile("http://youla.ria.su/upload_img.php", fileName);
                Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", System.Text.Encoding.UTF8.GetString(responseArray));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке картинки: \n" + ex.ToString());
            }
            return imgFileName;
        }
    }
}
