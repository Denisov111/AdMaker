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

namespace AdMakerM
{
    /// <summary>
    /// Логика взаимодействия для AddComp.xaml
    /// </summary>
    public partial class AddComp : Window
    {
        Global global;

        public AddComp(Global global)
        {
            InitializeComponent();
            this.global = global;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.VideoCards f = new Views.VideoCards(global);
            f.ShowDialog();
        }
    }
}
