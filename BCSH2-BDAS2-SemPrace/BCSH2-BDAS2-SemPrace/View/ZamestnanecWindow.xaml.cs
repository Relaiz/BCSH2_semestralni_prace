using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ZamestnanecWindow.xaml
    /// </summary>
    public partial class ZamestnanecWindow : Window
    {
        public Zamestnanec Zamestnanec { get; set; }

        public ZamestnanecWindow(Zamestnanec zamestnanec, String Pozice)
        {
            InitializeComponent();
            Zamestnanec = zamestnanec;

            DataContext = new ZamestnanecViewModel(zamestnanec, Pozice);

            klientsList.ItemsSource = ((ZamestnanecViewModel)DataContext).ListKlientu;
        }    
    }
}
