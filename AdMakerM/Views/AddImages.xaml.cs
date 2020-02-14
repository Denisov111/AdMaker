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
    /// Логика взаимодействия для AddImages.xaml
    /// </summary>
    public partial class AddImages : Window
    {
        Global global;
        Computer comp;

        public ObservableCollection<AdImage> Images { get; set; } = new ObservableCollection<AdImage>();

        public AddImages(Global global, Computer comp)
        {
            InitializeComponent();

            this.global = global;
            this.comp = comp;
            DataContext = new
            {
                ImageOptions = comp.ImagesPath,
                Local = this
            };

            //foreach (ProcessorSocket sock in Enum.GetValues(typeof(ProcessorSocket)))
            //{
            //    processorSocketComboBox.Items.Add(new ComboBoxItem() { Content = sock.ToString() });
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.Multiselect = true;
            //dlg.DefaultExt = ".txt"; // Default file extension
            //dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                //string filename = dlg.FileName;
                

                foreach(string fileName in dlg.FileNames)
                {
                    comp.ImagesPath.Add(new AdImage() { Path = fileName });
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
