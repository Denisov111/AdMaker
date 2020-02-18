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
    /// Логика взаимодействия для AddMama.xaml
    /// </summary>
    public partial class AddMama : Window
    {
        Global global;

        public string MBTitle { get; set; }
        public ProcessorSocket Socket { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<Motherboard> SelectedMB { get; set; } = new ObservableCollection<Motherboard>();

        public AddMama(Global global)
        {
            InitializeComponent();

            this.global = global;
            DataContext = new
            {
                MBOptions = global.MBOptions,
                Local = this
            };

            foreach (ProcessorSocket sock in Enum.GetValues(typeof(ProcessorSocket)))
            {
                processorSocketComboBox.Items.Add(new ComboBoxItem() { Content = sock.ToString() });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorSocket socket = (ProcessorSocket)processorSocketComboBox.SelectedIndex;
            
            Motherboard mb = new Motherboard()
            {
                Title = MBTitle,
                Socket = socket,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(mb);
            global.MBOptions.Add(mb);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var mb in mbDataGrid.SelectedItems)
            {
                Console.WriteLine(mb);
                Motherboard mb_ = ((Motherboard)mb).Clone();
                SelectedMB.Add(mb_);
            }
            Close();
        }
    }
}
