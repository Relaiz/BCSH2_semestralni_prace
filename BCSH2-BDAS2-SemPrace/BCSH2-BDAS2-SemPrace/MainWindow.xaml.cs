using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using BCSH2_BDAS2_SemPrace.ViewModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Text;
using System.Windows;

namespace BCSH2_BDAS2_SemPrace
{
    /// <summary>
    /// The entry point into the application, login window.
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginViewModel loginViewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeLoginWindow();
        }

        private void InitializeLoginWindow()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginViewModel = loginWindow.DataContext as LoginViewModel;

            // Подпишемся на событие успешного входа
            

            loginWindow.ShowDialog();
        }

       
    }
}