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
using System.Windows.Navigation;
using System.Windows.Shapes;
==
namespace BCSH2_SemestralniPrace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString;

        public MainWindow()
        {
            InitializeComponent();
            //string constr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA=(SID=BDAS)));" +
            //            "user id=stXXXXX;password=abcde;" +
            //            "Connection Timeout=120;Validate connection=true;Min Pool Size=4;";
            connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521)))(CONNECT_DATA=(SID=BDAS)));" +
                      "user id=st67094;password=Awphaiperbist1;" +
                      "Connection Timeout=30;Validate connection=true;Min Pool Size=4;";
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle the connect button click
            /* try
             {
                 using (var connection = new OracleConnection(connectionString))
                 {
                     connection.Open();
                     // Database is connected
                     // You can now perform database operations
                     DataGrid.Visibility = Visibility.Visible; // Show data grid, etc.
                 }
             }
             catch (OracleException ex)
             {
                 // Handle connection error
                 MessageBox.Show($"Connection Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
             }*/

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Проверка успешности подключения
                    if (connection.State == ConnectionState.Open)
                    {
                        MessageBox.Show("Подключение к базе данных успешно.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Теперь, если подключение успешно, вы можете выполнять операции с базой данных
                        DataGrid.Visibility = Visibility.Visible; // Показать таблицу данных и т.д.
                    }
                    else
                    {
                        MessageBox.Show("Не удалось установить подключение к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (OracleException ex)
            {
                // Обработка ошибки подключения
                MessageBox.Show($"Ошибка подключения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
