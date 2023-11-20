using BCSH2_BDAS2_SemPrace.DataBase;
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

namespace BCSH2_BDAS2_SemPrace
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            OracleDatabaseService db = new OracleDatabaseService();
            db.OpenConnection();
            OracleCommand cmd = new OracleCommand();
            //cmd.Connection = db.;
            //cmd.CommandText = "select name from counttest where id = 100";
            //cmd.CommandType = CommandType.Text;
            //OracleDataReader dr = cmd.ExecuteReader();
            //dr.Read();
            //lbl_info.Content = dr.GetString(0);
           // cmd.CommandText = "select ulice from adresa where id_adres = 41";
            string  query= "select ulice from adresa where id_adres = 41";
            DataTable userTable = db.ExecuteQuery(query);

            // Выводим содержимое DataTable в TextBox (замените textBox1 на имя вашего TextBox)
            if (userTable.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow row in userTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        sb.Append(item + " ");
                    }
                    sb.AppendLine();
                }
                ServerTextBox.Text = sb.ToString();
            }
            else
            {
                ServerTextBox.Text = "Нет данных";
            }

            db.CloseConnection();
        }
    }
}