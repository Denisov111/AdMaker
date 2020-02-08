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
    /// Логика взаимодействия для AddMemory.xaml
    /// </summary>
    public partial class AddMemory : Window
    {
        Global global;

        public string MemoryTitle { get; set; }
        public int Volume { get; set; }
        public MemoryType MemoryType { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<Memory> SelectedMemory { get; set; } = new ObservableCollection<Memory>();

        public AddMemory(Global global)
        {
            InitializeComponent();
            this.global = global;
            DataContext = new
            {
                MemoryOptions = global.MemoryOptions,
                Local = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MemoryType memoryType = 0;
            if (memoryTypeComboBox.SelectedIndex == 0)
                memoryType = MemoryType.DDR2;
            if (memoryTypeComboBox.SelectedIndex == 1)
                memoryType = MemoryType.DDR3;
            if (memoryTypeComboBox.SelectedIndex == 2)
                memoryType = MemoryType.DDR4;

            int volume = Int32.Parse(memoryVolComboBox.SelectedValue.ToString());

            Memory mem = new Memory()
            {
                Title = MemoryTitle,
                Volume = volume,
                MemoryType = memoryType,
                Price = Price
            };
            Console.WriteLine(mem);
            global.MemoryOptions.Add(mem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
