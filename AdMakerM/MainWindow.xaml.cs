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
using System.Collections.ObjectModel;

namespace AdMakerM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Global global;

        

        public MainWindow()
        {
            InitializeComponent();

            global = new Global(null);
            DataContext = global;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddComp f = new AddComp(global, null);
            f.ShowDialog();

            //MemoryOptionsColl = f.SelectedMemory;
            //memoryOptionsDataGrid.ItemsSource = null;
            //memoryOptionsDataGrid.ItemsSource = MemoryOptionsColl;
            //memoryOptionsDataGrid.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            global.Edit(((Button)sender).Tag.ToString());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            global.ShowAds(((Button)sender).Tag.ToString());
        }
    }
}
