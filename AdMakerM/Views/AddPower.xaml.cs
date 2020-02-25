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
    /// Логика взаимодействия для AddPower.xaml
    /// </summary>
    public partial class AddPower : Window
    {
        Global global;

        public string PowerSupplyTitle { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<PowerSupply> SelectedPowerSupply { get; set; } = new ObservableCollection<PowerSupply>();

        public AddPower(Global global)
        {
            InitializeComponent();

            this.global = global;
            DataContext = new
            {
                PowerOptions = global.PowerOptions,
                Local = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PowerSupply ps = new PowerSupply()
            {
                Title = PowerSupplyTitle,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(ps);
            global.PowerOptions.Add(ps);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var ps in powerCoolerDataGrid.SelectedItems)
            {
                Console.WriteLine(ps);
                PowerSupply ps_ = ((PowerSupply)ps).Clone();
                SelectedPowerSupply.Add(ps_);
            }
            Close();
        }
    }
}
