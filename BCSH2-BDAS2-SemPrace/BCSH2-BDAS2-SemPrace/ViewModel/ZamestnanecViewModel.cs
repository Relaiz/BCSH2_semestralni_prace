using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class ZamestnanecViewModel : INotifyPropertyChanged
    {
        private string _jmeno;
        private string _prijmeni;
        private string _telCislo;
        private string _email;
        private string _pozice;
        private string _pobocka;
        private string _status;
        private Zamestnanec _actualniZamestnanec;
        private ObservableCollection<Klient> _listKlientu;


        public ObservableCollection<Klient> ListKlientu
        {
            get { return _listKlientu; }
            set
            {
                _listKlientu = value;
                OnPropertyChanged(nameof(ListKlientu));
            }
        }
        public string Pozice
        {
            get => _pozice;
            set
            {
                _pozice = value;
                OnPropertyChanged(nameof(Pozice));
            }
        }

        public string Pobocka
        {
            get => _pobocka;
            set
            {
                _pobocka = value;
                OnPropertyChanged(nameof(Pobocka));
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
        public string TelCislo
        {
            get => _telCislo;
            set
            {
                _telCislo = value;
                OnPropertyChanged(nameof(TelCislo));
            }
        }

        public string Jmeno
        {
            get => _jmeno;
            set
            {
                _jmeno = value;
                OnPropertyChanged(nameof(Jmeno));
                OnPropertyChanged(nameof(CeleJmeno));
            }
        }

        public string Prijmeni
        {
            get => _prijmeni;
            set
            {
                _prijmeni = value;
                OnPropertyChanged(nameof(Prijmeni));
                OnPropertyChanged(nameof(CeleJmeno));
            }
        }

        public string CeleJmeno => $"{Jmeno} {Prijmeni}";
       

        private Klient _actualmiKlient;
        public Klient AktualniKlient
        {
            get { return _actualmiKlient; }
            set
            {
                _actualmiKlient = value;
                OnPropertyChanged(nameof(AktualniKlient));
            }
        }
        public Zamestnanec AktualniZamestnanec
        {
            get { return _actualniZamestnanec; }
            set
            {
                _actualniZamestnanec = value;
                OnPropertyChanged(nameof(AktualniZamestnanec));
            }
        }

        public ICommand PridatKlientCommand { get; }
        public ICommand UkazKlientCommand { get; }
        public ICommand ExitZamestnanecCommand { get; }
        public ICommand SmazatKlientCommand { get; }
        public ICommand UpravitKlientCommand { get; }
        public ICommand UpravitZamestnanecCommand { get; }

        private readonly OracleDatabaseService db;

        public ZamestnanecViewModel(Zamestnanec zamestnanec, String Pozice)
        {           
            db = new OracleDatabaseService();
            db.OpenConnection();
            AktualniZamestnanec = zamestnanec;
            GetPobockaName(AktualniZamestnanec.IdZamestnanec);
            GetStatus(AktualniZamestnanec.IdZamestnanec);           
            _jmeno = zamestnanec.Jmeno;
            _prijmeni = zamestnanec.Prijmeni;
            _email = zamestnanec.EmailZamestnanec;
            _telCislo = zamestnanec.TelefoniCislo;
            _pozice = Pozice;           
            _listKlientu = new ObservableCollection<Klient>();          
            PopulateKlientsList();           
            ExitZamestnanecCommand = new RelayCommand(Exit);
            UkazKlientCommand = new RelayCommand(UkazKlient);
            PridatKlientCommand = new RelayCommand(PridatKlient);
            SmazatKlientCommand = new RelayCommand(SmazatKlient);
            UpravitKlientCommand = new RelayCommand(UpravitKlient);
            UpravitZamestnanecCommand=new RelayCommand(UpravitZamestnanec);
        }

        private void UpravitZamestnanec(object parameter)
        {
            try
            {
                UpravitZamestnanecViewModel upravitZamestnanecViewModel = new UpravitZamestnanecViewModel(AktualniZamestnanec);
                UpravitZamestnanecWindow upravitZamestnanecWindow = new UpravitZamestnanecWindow(AktualniZamestnanec);
                upravitZamestnanecWindow.DataContext = upravitZamestnanecViewModel;
                upravitZamestnanecWindow.ShowDialog();
                GetPobockaName(AktualniZamestnanec.IdZamestnanec);
                GetStatus(AktualniZamestnanec.IdZamestnanec);
                Zamestnanec zamPomocna = AktualizovatZamestnanec(AktualniZamestnanec.IdZamestnanec);
                Jmeno= zamPomocna.Jmeno;
                Prijmeni= zamPomocna.Prijmeni;
                TelCislo=zamPomocna.TelefoniCislo;
                Email= zamPomocna.EmailZamestnanec;
                AktualniZamestnanec.Jmeno = zamPomocna.Jmeno;
                AktualniZamestnanec.Prijmeni = zamPomocna.Prijmeni;
                AktualniZamestnanec.TelefoniCislo = zamPomocna.TelefoniCislo;
                AktualniZamestnanec.EmailZamestnanec = zamPomocna.EmailZamestnanec;
                
                PopulateKlientsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private Zamestnanec AktualizovatZamestnanec(int zamId)
        {
            Zamestnanec zamestnanec = null;

            try
            {
                db.OpenConnection();

                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = db.Connection;
                    command.CommandText = "SELECT jmeno, prijmeni, telefoni_cislo, email_zamestnanec FROM zamestnanec WHERE id_zamestnanec = :zamId";
                    command.Parameters.Add("zamId", OracleDbType.Int32).Value = zamId; 

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            zamestnanec = new Zamestnanec
                            {
                                Jmeno = reader["jmeno"].ToString(),
                                Prijmeni = reader["prijmeni"].ToString(),
                                TelefoniCislo = reader["telefoni_cislo"].ToString(),
                                EmailZamestnanec = reader["email_zamestnanec"].ToString()
                            };
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

            return zamestnanec;
        }
        private void UpravitKlient(object parameter)
        {
            if (AktualniKlient == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte klienta ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    
                    int idKlient = db.GetKlientId(AktualniKlient.Jmeno, AktualniKlient.Prijmeni);
                    AktualniKlient.IdKlient = idKlient;
                    UpravitKlientViewModel upravitKlientViewModel = new UpravitKlientViewModel(AktualniKlient);
                    UpravitKlientWindow upravitKlientWindow = new UpravitKlientWindow(AktualniKlient);
                    upravitKlientWindow.DataContext=upravitKlientViewModel;
                    upravitKlientWindow.ShowDialog();
                    PopulateKlientsList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                
            }
        }
        
        private void SmazatKlient(object parameter)
        {
            if (AktualniKlient == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte klienta ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    db.OpenConnection();
                    using (OracleCommand command = new OracleCommand("SmazatKlient",db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value =AktualniKlient.Jmeno;
                        command.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value =AktualniKlient.Prijmeni;
                        command.ExecuteNonQuery();
                        
                    }
                    PopulateKlientsList();
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
        private void PridatKlient(object parameter)
        {            
            PridatKlientViewModel pridatKlientViewModel = new PridatKlientViewModel(AktualniZamestnanec,ListKlientu);
            PridatKlientWindow pridatKlientWindow = new PridatKlientWindow(AktualniZamestnanec,ListKlientu);
            pridatKlientWindow.DataContext = pridatKlientViewModel;
            pridatKlientWindow.Show();
            PopulateKlientsList();        
        }
       

        private void UkazKlient(object parameter)
        {
            if (AktualniKlient != null)
            {
                string jmeno = AktualniKlient.Jmeno;
                string prijmeni = AktualniKlient.Prijmeni;
                Klient klient = db.GetKlientByJmenoPrijmeni(jmeno, prijmeni);
                ShowKlientWindow showKlientWindow = new ShowKlientWindow(klient);
                showKlientWindow.DataContext = klient;
                showKlientWindow.Show();
            }

        }

        private void Exit(object parameter)
        {
            LoginWindow loginWindow = new LoginWindow();
            Window currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            zamExit(AktualniZamestnanec);         
            currentWindow?.Close();           
            loginWindow.Show();
        }
        private void ExitSuccessfulLogin(string username)
        {
            db.OpenConnection();
            string tempQuery = "DELETE FROM successful_logins WHERE user_name = :username";

            using (OracleCommand tempCmd = new OracleCommand(tempQuery, db.Connection))
            {
                tempCmd.Parameters.Add("user_name", OracleDbType.Varchar2).Value = username;
                tempCmd.ExecuteNonQuery();
            }
            db.CloseConnection();
        }

        
        private void zamExit(Zamestnanec zamestnanec)
        {
            try
            {
                string username = $"{zamestnanec.Jmeno} {zamestnanec.Prijmeni}";
                ExitSuccessfulLogin(username);
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
        private void GetPobockaName(decimal id_zamestnanec)
        {
            db.OpenConnection();
            OracleCommand cmd = db.Connection.CreateCommand();  // Создаем команду открытого соединения
            cmd.CommandText = "SELECT f.nazev FROM pobocka f WHERE f.id_pobocka= (SELECT z.pobocka_id_pobocka FROM zamestnanec z WHERE z.id_zamestnanec = :id_zamestnanec)";
            cmd.Parameters.Add("id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                string pobockaName = "";

                if (reader.Read())
                {
                    pobockaName = reader["nazev"].ToString();
                }
                reader.Close();  
                _pobocka= pobockaName;
            }
            finally
            {
                db.CloseConnection();
            }
        }
        private void GetStatus(decimal id_zamestnanec)
        {
            try
            {
                
            db.OpenConnection();
            OracleCommand cmd = db.Connection.CreateCommand();
            cmd.CommandText = "SELECT f.popis FROM status f WHERE f.id_status= (SELECT z.status_id_status FROM zamestnanec z WHERE z.id_zamestnanec = :id_zamestnanec)";
            cmd.Parameters.Add("id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;
                OracleDataReader reader = cmd.ExecuteReader();
                string status = "";
                if (reader.Read())
                {
                    status = reader["popis"].ToString();
                }
                reader.Close();
                _status = status;
            }
            finally
            {
                db.CloseConnection();
            }
        }
        

       /* public List<Klient> GetHierarchyInfoFromDatabase(int id_zamestnanec)
        {
            try
            {
                db.OpenConnection();
                List<Klient> result = new List<Klient>();
                using (OracleCommand cmd = new OracleCommand("GetHierarchyInfo", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("p_id_zamestnanec", OracleDbType.Decimal).Value = id_zamestnanec;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string full_name = reader["full_name"].ToString();
                            string[] names = full_name.Split(' ');

                            string firstName = names[0];
                            string lastName = names[1];

                            result.Add(new Klient { Jmeno = firstName, Prijmeni = lastName });
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                db.CloseConnection();
            }
        }       */
        private void PopulateKlientsList()
        {

            var (clients, _) = db.GetHierarchyInfoFromDatabase(AktualniZamestnanec.IdZamestnanec);
            if (clients != null)
            {
                Console.WriteLine($"Pocet nalezenych klientu: {clients.Count}");
                _listKlientu.Clear();
            if (clients != null)
            {
                foreach (var klient in clients)
                {
                    _listKlientu.Add(klient);
                }
            }
            }
            else
            {
                Console.WriteLine("Prazdny seznam Klientu.");
            }

            OnPropertyChanged(nameof(ListKlientu));
        }
    }
}
