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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button clicked.");  

            // Получаем DataContext
            LoginViewModel viewModel = DataContext as LoginViewModel;

            if (viewModel == null)
            {
                Console.WriteLine("ViewModel is null.");  
                return;
            }

            // Вызываем метод LoginCommand.Execute
            if (viewModel.LoginCommand.CanExecute(null))
            {
                Console.WriteLine("Executing LoginCommand.");  
                viewModel.LoginCommand.Execute(null);
                
            }
            else
            {
                Console.WriteLine("LoginCommand cannot execute.");  
            }
        }

        
        

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }
    }
}
