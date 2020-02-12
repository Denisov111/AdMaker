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
    /// Логика взаимодействия для AddHDD.xaml
    /// </summary>
    public partial class AddHDD : Window
    {
        private Global global;

        public string HDDTitle { get; set; } = "WD";
        public int Volume { get; set; } = 500;
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<HDD> SelectedHDD { get; set; } = new ObservableCollection<HDD>();

        public AddHDD(Global global)
        {
            InitializeComponent();
            this.global = global;

            DataContext = new
            {
                HDDOptions = global.HDDOptions,
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
            HDD hdd = new HDD()
            {
                Title = HDDTitle,
                Volume = Volume,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(hdd);
            global.HDDOptions.Add(hdd);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            foreach (var hdd in hddDataGrid.SelectedItems)
            {
                Console.WriteLine(hdd);
                HDD adapter = ((HDD)hdd).Clone();
                SelectedHDD.Add(adapter);
            }
            Close();
        }
    }
}
