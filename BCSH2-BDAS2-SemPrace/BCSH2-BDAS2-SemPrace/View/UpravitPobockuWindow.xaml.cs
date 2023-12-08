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
    
    /// </summary>
    public partial class UpravitPobockuWindow : Window
    {
        public  UpravitPobockuWindow(Pobocka pobocka)
        {
            InitializeComponent();
            DataContext = pobocka;
        }
    }
}
