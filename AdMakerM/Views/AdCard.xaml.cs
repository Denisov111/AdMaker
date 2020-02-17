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

namespace AdMakerM.Views
{
    /// <summary>
    /// Логика взаимодействия для AdCard.xaml
    /// </summary>
    public partial class AdCard : Window
    {
        Ad ad;
        public AdCard(Ad ad)
        {
            InitializeComponent();
            this.ad = ad;
            DataContext = this;
        }
    }
}
