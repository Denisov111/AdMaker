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
    /// Логика взаимодействия для AddCase.xaml
    /// </summary>
    public partial class AddCase : Window
    {
        Global global;

        public string CaseTitle { get; set; }
        public decimal Price { get; set; } = 1000;
        public ObservableCollection<Case> SelectedCase { get; set; } = new ObservableCollection<Case>();

        public AddCase(Global global)
        {
            InitializeComponent();

            this.global = global;
            DataContext = new
            {
                CaseOptions = global.CaseOptions,
                Local = this
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Case cs = new Case()
            {
                Title = CaseTitle,
                Price = Price,
                Guid = Guid.NewGuid().ToString()
            };
            Console.WriteLine(cs);
            global.CaseOptions.Add(cs);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var cs in caseDataGrid.SelectedItems)
            {
                Console.WriteLine(cs);
                Case cs_ = ((Case)cs).Clone();
                SelectedCase.Add(cs_);
            }
            Close();
        }
    }
}
