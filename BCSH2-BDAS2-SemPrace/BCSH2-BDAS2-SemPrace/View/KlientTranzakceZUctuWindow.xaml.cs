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
using BCSH2_BDAS2_SemPrace.ViewModel;

namespace BCSH2_BDAS2_SemPrace.View
{
    /// <summary>
    /// Interaction logic for KlientTranzakceZUctuWindow.xaml
    /// </summary>
    public partial class KlientTranzakceZUctuWindow : Window
    {
        public KlientTranzakceZUctuWindow(Ucet ucet)
        {
            InitializeComponent();
            DataContext = new KlientTranzakceZUctuViewModel(ucet);
        }
    }
}
