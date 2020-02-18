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
    /// Логика взаимодействия для AddCPUCooler.xaml
    /// </summary>
    public partial class AddCPUCooler : Window
    {
        Global global;

        public string CPUCoolerTitle { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<ProcessorCooler> SelectedCPUCooler { get; set; } = new ObservableCollection<ProcessorCooler>();

        public AddCPUCooler(Global global)
        {
            InitializeComponent();

            this.global = global;
            DataContext = new
            {
                CPUCoolerOptions = global.CPUCoolerOptions,
                Local = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessorCooler cc = new ProcessorCooler()
            {
                Title = CPUCoolerTitle,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(cc);
            global.CPUCoolerOptions.Add(cc);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var cc in cpuCoolerDataGrid.SelectedItems)
            {
                Console.WriteLine(cc);
                ProcessorCooler cc_ = ((ProcessorCooler)cc).Clone();
                SelectedCPUCooler.Add(cc_);
            }
            Close();
        }
    }
}
