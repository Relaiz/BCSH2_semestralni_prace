using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
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
    public class ManazerViewModel : INotifyPropertyChanged
    {
        private string _jmeno;
        private string _prijmeni;
        private string _telCislo;
        private string _email;
        private string _pozice;
        private string _pobocka;
        private string _status;
        private string _adresa;
        private Zamestnanec _actualniManazer;
        private ObservableCollection<Zamestnanec> _listBankeru;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly OracleDatabaseService db;
        public ICommand PridatBankereCommand { get; }
        public ICommand UkazBankereCommand { get; }
        public ICommand ExitManazerCommand { get; }
        public ICommand SmazatBankereCommand { get; }
        public ICommand UpravitBankereCommand { get; }
        public ICommand UpravitZamestnanecCommand { get; }

        public ManazerViewModel(Zamestnanec zamestnanec, string pozice)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            AktualniManazer = zamestnanec;
            GetPobockaName(AktualniManazer.IdZamestnanec);
            GetStatus(AktualniManazer.IdZamestnanec);
            _jmeno = zamestnanec.Jmeno;
            _prijmeni = zamestnanec.Prijmeni;
            _email = zamestnanec.EmailZamestnanec;
            _telCislo = zamestnanec.TelefoniCislo;
            _pozice = pozice;
            _listBankeru = new ObservableCollection<Zamestnanec>();
            PopulateBankereList();
            ExitManazerCommand= new RelayCommand(Exit);
            UkazBankereCommand = new RelayCommand(UkazBankere);
            SmazatBankereCommand = new RelayCommand(SmazatBankere);
            PridatBankereCommand = new RelayCommand(PridatBankere);
            UpravitBankereCommand = new RelayCommand(UpravitBankere);
            UpravitZamestnanecCommand = new RelayCommand(UpravitZamestnanec);


        }

        
        public string Adresa
        {
            get { return _adresa; }
            set
            {
                if (_adresa != value)
                {
                    _adresa = value;
                    OnPropertyChanged(nameof(Adresa));
                }
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

        public Zamestnanec AktualniManazer
        {
            get { return _actualniManazer; }
            set
            {
                _actualniManazer = value;
                OnPropertyChanged(nameof(AktualniManazer));
            }
        }
        public ObservableCollection<Zamestnanec> ListBankeru
        {
            get { return _listBankeru; }
            set
            {
                _listBankeru = value;
                OnPropertyChanged(nameof(ListBankeru));
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
        private Zamestnanec _actualniBanker;
        public Zamestnanec AktualniBanker
        {
            get { return _actualniBanker; }
            set
            {
                _actualniBanker = value;
                OnPropertyChanged(nameof(AktualniBanker));
            }
        }
        private void GetPobockaName(decimal id_zamestnanec)
        {
            db.OpenConnection();
            OracleCommand cmd = db.Connection.CreateCommand();
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
                _pobocka = pobockaName;
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

        private void Exit(object parameter)
        {
            LoginWindow loginWindow = new LoginWindow();
            Window currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            ManazerExit(AktualniManazer);
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


        private void ManazerExit(Zamestnanec zamestnanec)
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
        private void UkazBankere(object parameter)
        {
            if (AktualniBanker != null)
            {
                string jmeno = AktualniBanker.Jmeno;
                string prijmeni = AktualniBanker.Prijmeni;
                Zamestnanec banker = db.GetBankerByJmenoPrijmeni(jmeno, prijmeni);
                ShowBankerWindow showBankerWindow = new ShowBankerWindow(banker);
                showBankerWindow.DataContext=banker;
                showBankerWindow.Show();
            }

        }
        private void SmazatBankere(object parameter)
        {
            if (AktualniBanker == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte bankere ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    db.OpenConnection();
                    using (OracleCommand command = new OracleCommand("SmazatBankere", db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = AktualniBanker.Jmeno;
                        command.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = AktualniBanker.Prijmeni;
                        command.ExecuteNonQuery();

                    }
                    PopulateBankereList();
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
        private void PridatBankere(object parameter)
        {
            PridatBankerViewModel pridatBankerViewModel=new PridatBankerViewModel(AktualniManazer);
            PridatBankerWindow pridatBankerWindow = new PridatBankerWindow(AktualniBanker);
            pridatBankerWindow.DataContext = pridatBankerViewModel;
            pridatBankerWindow.ShowDialog();
            PopulateBankereList();
        }
        private void UpravitBankere(object parameter)
        {
            if (AktualniBanker == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte bankera ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {

                    int idBanker = db.GetZamestnanecId(AktualniBanker.Jmeno, AktualniBanker.Prijmeni);
                    AktualniBanker.IdZamestnanec = idBanker;
                    UpravitBankerViewModel upravitBankerViewModel = new UpravitBankerViewModel(AktualniBanker);
                    UpravitBankerWindow upravitBankerWindow = new UpravitBankerWindow(AktualniBanker);
                    upravitBankerWindow.DataContext = upravitBankerViewModel;
                    upravitBankerWindow.ShowDialog();
                    PopulateBankereList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
        }
        private void UpravitZamestnanec(object parameter)
        {
            try
            {
                UpravitZamestnanecViewModel upravitZamestnanecViewModel = new UpravitZamestnanecViewModel(AktualniManazer);
                UpravitZamestnanecWindow upravitZamestnanecWindow = new UpravitZamestnanecWindow(AktualniManazer);
                upravitZamestnanecWindow.DataContext = upravitZamestnanecViewModel;
                upravitZamestnanecWindow.ShowDialog();
                GetPobockaName(AktualniManazer.IdZamestnanec);
                GetStatus(AktualniManazer.IdZamestnanec);
                Zamestnanec zamPomocna = AktualizovatZamestnanec(AktualniManazer.IdZamestnanec);
                Jmeno = zamPomocna.Jmeno;
                Prijmeni = zamPomocna.Prijmeni;
                TelCislo = zamPomocna.TelefoniCislo;
                Email = zamPomocna.EmailZamestnanec;
                AktualniManazer.Jmeno = zamPomocna.Jmeno;
                AktualniManazer.Prijmeni = zamPomocna.Prijmeni;
                AktualniManazer.TelefoniCislo = zamPomocna.TelefoniCislo;
                AktualniManazer.EmailZamestnanec = zamPomocna.EmailZamestnanec;

                PopulateBankereList();
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
        private void PopulateBankereList()
        {

            var (_, bankere) = db.GetHierarchyInfoFromDatabase(AktualniManazer.IdZamestnanec);
            if (bankere != null)
            {
                Console.WriteLine($"Pocet nalezenych bankeru: {bankere.Count}");
                _listBankeru.Clear();
                if (bankere != null)
                {
                    foreach (var banker in bankere)
                    {
                        _listBankeru.Add(banker);
                    }
                }
            }
            else
            {
                Console.WriteLine("Prazdny seznam Klientu.");
            }

            OnPropertyChanged(nameof(ListBankeru));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
