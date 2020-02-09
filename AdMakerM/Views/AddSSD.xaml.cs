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
    /// Логика взаимодействия для AddSSD.xaml
    /// </summary>
    public partial class AddSSD : Window
    {
        Global global;

        public string SSDTitle { get; set; } = "Patriot";
        public int Volume { get; set; } = 120;
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<SSD> SelectedSSD { get; set; } = new ObservableCollection<SSD>();



        public AddSSD(Global global)
        {
            InitializeComponent();
            this.global = global;
            DataContext = new
            {
                SSDOptions = global.SSDOptions,
                Local = this
            };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var text = ((Button)(FrameworkElement)sender).Content.ToString();
            volumeTextBox.Text = text;
            Volume = Int32.Parse(text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SSD ssd = new SSD()
            {
                Title = SSDTitle,
                Volume = Volume,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(ssd);
            global.SSDOptions.Add(ssd);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            foreach (var ssd in ssdDataGrid.SelectedItems)
            {
                Console.WriteLine(ssd);
                SSD adapter = ((SSD)ssd).Clone();
                SelectedSSD.Add(adapter);
            }
            Close();
        }
    }
}
