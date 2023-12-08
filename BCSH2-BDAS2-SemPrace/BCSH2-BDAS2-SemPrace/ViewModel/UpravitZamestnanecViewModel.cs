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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class UpravitZamestnanecViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        private string _jmeno;
        private string _prijmeni;
        private string _telCislo;
        private string _email;
        private int id;
        private Zamestnanec aktualniZam;


        public ICommand UpravitZamCommand { get; }

        public UpravitZamestnanecViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            aktualniZam = new Zamestnanec();
            AktualniZam = zamestnanec;
            id = AktualniZam.IdZamestnanec;



            Jmeno = AktualniZam.Jmeno;
            Prijmeni = AktualniZam.Prijmeni;

            Email = AktualniZam.EmailZamestnanec;
            TelCislo = AktualniZam.TelefoniCislo;

            UpravitZamCommand = new RelayCommand(Upravit);



        }
        public Zamestnanec AktualniZam
        {
            get => aktualniZam;
            set
            {
                aktualniZam = value;
                OnPropertyChanged(nameof(AktualniZam));
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

                using (OracleCommand command = new OracleCommand("UPDATE_ZAMESTNANEC", db.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_id_zamestnanec ", OracleDbType.Int32).Value = AktualniZam.IdZamestnanec;                   
                    command.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Jmeno;
                    command.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Prijmeni;
                    command.Parameters.Add("p_email_zamestnanec", OracleDbType.Varchar2).Value = Email;                   
                    command.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelCislo;
                    command.Parameters.Add("p_pobocka_id_pobocka", OracleDbType.Int32).Value = AktualniZam.PobockaIdPobocka;
                    command.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = AktualniZam.StatusIdStatus;                    
                    command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = Email;
                    command.ExecuteNonQuery();
                }
                MessageBoxResult result = MessageBox.Show("Banker byl upraven.", "Uspech", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
        
                          {  Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    if (actualnWindow != null)
                    {
                        actualnWindow.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
               
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
