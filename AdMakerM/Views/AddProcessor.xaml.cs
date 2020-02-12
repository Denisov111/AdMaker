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
    /// Логика взаимодействия для AddProcessor.xaml
    /// </summary>
    public partial class AddProcessor : Window
    {
        Global global;

        public string ProcessorTitle { get; set; }
        public ProcessorSocket Socket { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<Processor> SelectedProcessors { get; set; } = new ObservableCollection<Processor>();

        public AddProcessor(Global global)
        {
            InitializeComponent();

            this.global = global;
            DataContext = new
            {
                ProcessorOptions = global.ProcessorOptions,
                Local = this
            };

            foreach (ProcessorSocket sock in Enum.GetValues(typeof(ProcessorSocket)))
            {
                processorSocketComboBox.Items.Add(new ComboBoxItem() { Content = sock.ToString()});
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorSocket socket = (ProcessorSocket)processorSocketComboBox.SelectedIndex;
            //if (processorSocketComboBox.SelectedIndex == 0)
            //    memoryType = MemoryType.DDR2;
            //if (processorSocketComboBox.SelectedIndex == 1)
            //    memoryType = MemoryType.DDR3;
            //if (processorSocketComboBox.SelectedIndex == 2)
            //    memoryType = MemoryType.DDR4;


            Processor pr = new Processor()
            {
                Title = ProcessorTitle,
                Socket = socket,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(pr);
            global.ProcessorOptions.Add(pr);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var pr in processorsDataGrid.SelectedItems)
            {
                Console.WriteLine(pr);
                Processor pr_ = ((Processor)pr).Clone();
                SelectedProcessors.Add(pr_);
            }
            Close();
        }
    }
}
