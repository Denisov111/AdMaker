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

namespace AdMakerM
{
    /// <summary>
    /// Логика взаимодействия для AddComp.xaml
    /// </summary>
    public partial class AddComp : Window
    {
        Global global;

        public string AdTitle { get; set; } = "Игровой ПК ";
        public string AdDesc { get; set; } = "Игровой ПК ";
        public ObservableCollection<VideoAdapter> VideoAdaptersColl { get; set; } = new ObservableCollection<VideoAdapter>();
        public ObservableCollection<Memory> MemoryOptionsColl { get; set; } = new ObservableCollection<Memory>();
        public int Memory { get; set; } = 0;
        public int TDP { get; set; } = 0;
        public decimal Price { get; set; } = 0;

        public AddComp(Global global)
        {
            InitializeComponent();
            this.global = global;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.VideoCards f = new Views.VideoCards(global);
            f.ShowDialog();
            VideoAdaptersColl = f.SelectedVideo;
            videoAdaptersDataGrid.ItemsSource = null;
            videoAdaptersDataGrid.ItemsSource = VideoAdaptersColl;
            videoAdaptersDataGrid.Items.Refresh();
        }

        private void AddCompButton_Click(object sender, RoutedEventArgs e)
        {
            Computer comp = new Computer()
            {
                Title = AdTitle,
                Descriptrion = AdDesc,
                Price = Price
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Views.AddMemory f = new Views.AddMemory(global);
            f.ShowDialog();
            MemoryOptionsColl = f.SelectedMemory;
            memoryOptionsDataGrid.ItemsSource = null;
            memoryOptionsDataGrid.ItemsSource = MemoryOptionsColl;
            memoryOptionsDataGrid.Items.Refresh();
        }
    }
}
