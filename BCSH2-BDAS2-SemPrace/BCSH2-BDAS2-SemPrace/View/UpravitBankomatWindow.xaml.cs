using BCSH2_BDAS2_SemPrace.Model;
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

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Логика взаимодействия для UpravitBankomatWindow.xaml
    /// </summary>
    public partial class UpravitBankomatWindow : Window
    {
        public UpravitBankomatWindow(Bankomat bankomat)
        {
            InitializeComponent();
            DataContext = bankomat;
        }
    }
}
