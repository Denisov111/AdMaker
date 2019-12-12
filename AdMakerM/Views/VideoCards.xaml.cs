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

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для VideoCards.xaml
    /// </summary>
    public partial class VideoCards : Window
    {
        Global global;

        public string CardTitle { get; set; } = "..........";
        public int Memory { get; set; } = 1024;
        public int TDP { get; set; } = 120;
        public decimal Price { get; set; } = 5990;

        public VideoCards(Global global)
        {
            InitializeComponent();
            this.global = global;
            DataContext = new
            {
                VideoAdapters = global.VideoAdapters,
                Local = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            VideoAdapter va = new VideoAdapter()
            {
                Title = CardTitle,
                Memory = Memory,
                TDP=TDP,
                Price=Price
            };
            Console.WriteLine(va);
            global.VideoAdapters.Add(va);

            //global.SaveAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CardTitle = "Geforce ";
        }
    }
}
