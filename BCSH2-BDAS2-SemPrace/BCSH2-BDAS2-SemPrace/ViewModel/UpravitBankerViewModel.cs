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
    public class UpravitBankerViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        private string _jmeno;
        private string _prijmeni;
        private string _telCislo;
        private string _email;
        private Zamestnanec aktualniBanker;
        public ObservableCollection<Status> Statusy { get; set; }
        public ObservableCollection<Pobocka> Pobocky { get; set; }
        public int SelectedStatusId { get; set; }
        public int SelectedPobockaId { get; set; }
        public ICommand UpravitCommand { get; }
        private int id;
        public UpravitBankerViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Statusy = new ObservableCollection<Status>();
            Pobocky = new ObservableCollection<Pobocka>();
            LoadStatusy();
            LoadPobocky();        
            id = zamestnanec.IdZamestnanec;
            Zamestnanec bankerData= db.LoadZamestnanecDataById(id);
            AktualniBanker = bankerData;          
            Jmeno = zamestnanec.Jmeno;
            Prijmeni = zamestnanec.Prijmeni;
            AktualniBanker.IdZamestnanec = id;
            TelCislo = AktualniBanker.TelefoniCislo;
            Email = AktualniBanker.EmailZamestnanec;
            SelectedPobockaId = (int)AktualniBanker.PobockaIdPobocka;
            SelectedStatusId = AktualniBanker.StatusIdStatus;
            
            UpravitCommand = new RelayCommand(Upravit);
            //id = AktualniZam.IdZamestnanec;
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
                    command.Parameters.Add("p_id_zamestnanec ", OracleDbType.Int32).Value = AktualniBanker.IdZamestnanec;
                    command.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Jmeno;
                    command.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Prijmeni;
                    command.Parameters.Add("p_email_zamestnanec", OracleDbType.Varchar2).Value = Email;
                    command.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelCislo;                   
                    command.Parameters.Add("p_pobocka_id_pobocka", OracleDbType.Int32).Value = SelectedPobockaId;
                    command.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;
                    command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = Email;
                    command.ExecuteNonQuery();
                }
                MessageBoxResult result = MessageBox.Show("Banker byl upraven.", "Uspech", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public Zamestnanec AktualniBanker
        {
            get => aktualniBanker;
            set
            {
                aktualniBanker = value;
                OnPropertyChanged(nameof(AktualniBanker));
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
        private void LoadStatusy()
        {
            try
            {
                db.OpenConnection();

                string query = "SELECT id_status, popis FROM status";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Status status = new Status
                            {
                                IdStatus = Convert.ToInt32(reader["id_status"]),
                                Popis = reader["popis"].ToString()
                            };

                            Statusy.Add(status);
                        }
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



        private void LoadPobocky()
        {
            try
            {
                db.OpenConnection();
                string query = "SELECT id_pobocka, nazev FROM pobocka";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pobocka pobocka = new Pobocka
                            {
                                IdPobocka = Convert.ToInt32(reader["id_pobocka"]),
                                Nazev = reader["nazev"].ToString()
                            };

                            Pobocky.Add(pobocka);
                        }
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
    }
}
