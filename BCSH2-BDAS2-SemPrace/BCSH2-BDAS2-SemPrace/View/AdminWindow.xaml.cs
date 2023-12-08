using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.ViewModel;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(Zamestnanec zamestnanec)
        {
            InitializeComponent();
            AdminViewModel adminViewModel = new AdminViewModel(zamestnanec);
            
            this.DataContext = adminViewModel;
            zamList.ItemsSource = ((AdminViewModel)DataContext).Zamestnanci;
            klientList.ItemsSource = ((AdminViewModel)DataContext).Klienti;
        }

       
    }
}
