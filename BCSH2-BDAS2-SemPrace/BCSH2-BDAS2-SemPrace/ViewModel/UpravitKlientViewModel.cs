using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class UpravitKlientViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        private string _jmeno;
        private string _prijmeni;
        private string _telCislo;
        private string _email;
        private long _cisloPrukazu;
        private Klient _klientUprava;
        private int id;
        public ICommand UpravitCommand { get; }

        public UpravitKlientViewModel(Klient klient)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            id = klient.IdKlient;
            Klient klientData = db.LoadKlientDataById(id);
            KlientUprava = klientData;
            KlientUprava.IdKlient = id;
            Jmeno = klient.Jmeno;
            Prijmeni = klient.Prijmeni;
            
            Email = KlientUprava.KlientEmail;
            TelCislo = KlientUprava.TelefoniCislo;
            CisloPrukazu = KlientUprava.CisloPrukazu;
            UpravitCommand = new RelayCommand(Upravit);



        }
        public long CisloPrukazu
        {
            get => _cisloPrukazu;
            set
            {
                _cisloPrukazu = value;
                OnPropertyChanged(nameof(CisloPrukazu));
            }
        }
        public string TelCislo
        {
            get => _telCislo;
            set
            {
                _telCislo = value;
                OnPropertyChanged(nameof(TelCislo));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public Klient KlientUprava
        {
            get { return _klientUprava; }
            set
            {
                _klientUprava = value;
                OnPropertyChanged(nameof(KlientUprava));
            }
        }
        public string Jmeno
        {
            get => _jmeno;
            set
            {
                _jmeno = value;
                OnPropertyChanged(nameof(Jmeno));
            }
        }

        public string Prijmeni
        {
            get => _prijmeni;
            set
            {
                _prijmeni = value;
                OnPropertyChanged(nameof(Prijmeni));
            }
        }


        private void Upravit(object parameter)
        {
            try
            {
                db.OpenConnection();
                //Klient klient = db.GetKlientByJmenoPrijmeni(KlientUprava.Jmeno, KlientUprava.Prijmeni);
               // KlientUprava.TelefoniCislo = klient.TelefoniCislo;

                using (OracleCommand command = new OracleCommand("UPDATE_KLIENT", db.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_id_klient", OracleDbType.Int32).Value = KlientUprava.IdKlient;
                    command.Parameters.Add("p_cislo_prukazu", OracleDbType.Decimal).Value = CisloPrukazu;
                    command.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Jmeno;
                    command.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Prijmeni;
                    command.Parameters.Add("p_klient_email", OracleDbType.Varchar2).Value = Email;
                    command.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelCislo;
                    command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = Email;
                    command.ExecuteNonQuery();
                }
                MessageBoxResult result = MessageBox.Show("Klient byl upraven.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    if (actualnWindow != null)
                    {
                        actualnWindow.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetKlientId: " + ex.Message);
               
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
