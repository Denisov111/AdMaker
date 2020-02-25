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
            this.Ad = ad;
            if(ad.Articul==0)
            {
                MessageBox.Show("Забыли сохранить объявления");
                Close(); 
            }
            DataContext = this;

            SetProducts();
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
            string tag = ((FrameworkElement)sender).Tag.ToString();
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
    }
}
