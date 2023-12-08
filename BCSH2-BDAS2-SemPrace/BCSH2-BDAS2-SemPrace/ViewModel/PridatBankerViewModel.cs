using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Shapes;
using System.Xml.Linq;
using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows.Input;
using System.Windows;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class PridatBankerViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly OracleDatabaseService db;
        private Adresa _adresa = new Adresa();
        private string _psc;
        private string _ulice;
        private string _stat;
        private string _mesto;
        private string _cisloPopisne;
        private string _heslo;
        private string _email;
        private string _telCislo;
        private string _jmeno;
        private string _prijmeni;
        private int? _mzda;
        private Zamestnanec _asignManazer;
        private Zamestnanec _novyBanker;
        public int SelectedStatusId { get; set; }
        public int SelectedPobockaId { get; set; }
        public ObservableCollection<Status> Statusy { get; set; }
        public ObservableCollection<Pobocka> Pobocky { get; set; }
        public ICommand PridatBankerCommand { get; }
        public PridatBankerViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Heslo = "abcde";
            Mzda = 20000;
            _novyBanker = new Zamestnanec();
            AsignManazer = zamestnanec;
            PridatBankerCommand = new RelayCommand(PridatBanker);
            Statusy = new ObservableCollection<Status>();
            Pobocky = new ObservableCollection<Pobocka>();
            LoadStatusy();
            LoadPobocky();

        }
        public string Heslo
        {
            get { return _heslo; }
            set
            {
                if (_heslo != value)
                {
                    _heslo = value;
                    OnPropertyChanged(Heslo);
                }
            }
        }
        public int? Mzda
        {
            get { return _mzda; }
            set
            {
                if (_mzda != value)
                {
                    _mzda = value;
                    OnPropertyChanged(nameof(Mzda));
                }
            }
        }
        private Zamestnanec AsignManazer
        {
            get { return _asignManazer; }
            set
            {
                if (_asignManazer != value)
                {
                    _asignManazer = value;
                    OnPropertyChanged(nameof(AsignManazer));
                }
            }
        }
        private Zamestnanec NovyBanker
        {
            get { return _novyBanker; }
            set
            {
                if (_novyBanker != value)
                {
                    _novyBanker = value;
                    OnPropertyChanged(nameof(NovyBanker));
                }
            }
        }
        public string Stat
        {
            get { return _stat; }
            set
            {
                if (_stat != value)
                {
                    _stat = value;
                    OnPropertyChanged(Stat);
                }
            }
        }
        public string Mesto
        {
            get { return _mesto; }
            set
            {
                if (_mesto != value)
                {
                    _mesto = value;
                    OnPropertyChanged(Mesto);
                }
            }
        }
        public string Ulice
        {
            get { return _ulice; }
            set
            {
                if (_ulice != value)
                {
                    _ulice = value;
                    OnPropertyChanged(Ulice);
                }
            }

        }
        public string CisloPopisne
        {
            get { return _cisloPopisne; }
            set
            {
                if (_cisloPopisne != value)
                {
                    _cisloPopisne = value;
                    OnPropertyChanged(CisloPopisne);
                }
            }

        }
        public string PSC
        {
            get { return _psc; }
            set
            {
                if (_psc != value)
                {
                    _psc = value;
                    OnPropertyChanged(PSC);
                }
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
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(Email);
                }
            }
        }

        public string TelCislo
        {
            get { return _telCislo; }
            set
            {
                if (_telCislo != value)
                {
                    _telCislo = value;
                    OnPropertyChanged(TelCislo);
                }
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
        public int GetAddressId(string ulice, string mesto, string cisloPopisne, string psc, string stat)
        {
            int? id_adres = null;

            try
            {
                db.OpenConnection();
                string query = "BEGIN :result := GetAddressId(:ulice, :mesto, :cislo_popisne, :psc, :stat); END;";
                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    cmd.Parameters.Add("result", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("ulice", OracleDbType.Varchar2).Value = ulice;
                    cmd.Parameters.Add("mesto", OracleDbType.Varchar2).Value = mesto;
                    cmd.Parameters.Add("cislo_popisne", OracleDbType.Varchar2).Value = cisloPopisne;
                    cmd.Parameters.Add("psc", OracleDbType.Char).Value = psc;
                    cmd.Parameters.Add("stat", OracleDbType.Varchar2).Value = stat;
                    cmd.ExecuteNonQuery();
                    var result = cmd.Parameters["result"].Value;
                    if (result != DBNull.Value)
                    {
                        OracleDecimal oracleDecimal = (OracleDecimal)result;
                        if (!oracleDecimal.IsNull)
                        {
                            id_adres = oracleDecimal.ToInt32();
                        }
                        else
                        {
                            id_adres = 0;
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

            return (int)id_adres;
        }
        private void CheckOrAddAdresa()
        {
            try
            {
                string ulice = Ulice;
                string mesto = Mesto;
                string cisloPopisne = CisloPopisne;
                string psc = PSC;
                string stat = Stat;
                int? id_adres = GetAddressId(ulice, mesto, cisloPopisne, psc, stat);
                db.OpenConnection();
                if (id_adres == null || id_adres == 0)
                {
                    string query = "BEGIN AddAddress(:ulice, :mesto, :cislo_popisne, :psc, :stat, :id_adres); END;";
                    using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                    {
                        cmd.Parameters.Add("ulice", OracleDbType.Varchar2).Value = ulice;
                        cmd.Parameters.Add("mesto", OracleDbType.Varchar2).Value = mesto;
                        cmd.Parameters.Add("cislo_popisne", OracleDbType.Varchar2).Value = cisloPopisne;
                        cmd.Parameters.Add("psc", OracleDbType.Char).Value = psc;
                        cmd.Parameters.Add("stat", OracleDbType.Varchar2).Value = stat;
                        cmd.Parameters.Add("id_adres", OracleDbType.Int32).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        if (cmd.Parameters["id_adres"].Value != DBNull.Value)
                        {
                            OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["id_adres"].Value;
                            id_adres = oracleDecimal.ToInt32();
                            _adresa = new Adresa
                            {
                                IdAdres = id_adres.Value,
                                Ulice = ulice,
                                Mesto = mesto,
                                CisloPopisne = cisloPopisne,
                                PSC = psc,
                                Stat = stat
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
        }
        private void InsertLoginData()
        {
            try
            {
                db.OpenConnection();

                using (OracleCommand cmd = new OracleCommand("AddLogin", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_zamestnanec_id_zamestnanec", OracleDbType.Int32).Value = NovyBanker.IdZamestnanec;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = NovyBanker.EmailZamestnanec;
                    cmd.Parameters.Add("p_heslo", OracleDbType.Varchar2).Value = Heslo;
                    cmd.Parameters.Add("p_is_admin", OracleDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_klient_id_klient", OracleDbType.Int32).Value = null ;
                    cmd.Parameters.Add("p_id_file", OracleDbType.Int32).Value = null;
                    cmd.Parameters.Add("p_id_klient", OracleDbType.Int32).Value = null;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Success adding login data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding login data: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        private void PridatBanker(object parameter)
        {
            CheckOrAddAdresa();
            try
            {
                db.OpenConnection();
                int idBanker = 0;
                using (OracleCommand cmd = new OracleCommand("AddZamestnanec", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Jmeno;
                    cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Prijmeni;
                    cmd.Parameters.Add("p_mzda", OracleDbType.Int32).Value = Mzda;
                    cmd.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelCislo;
                    cmd.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = _adresa.IdAdres;
                    cmd.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = AsignManazer.BankIdBank;
                    cmd.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;
                    cmd.Parameters.Add("p_zamestnanec_id_zamestnanec1", OracleDbType.Int32).Value = AsignManazer.IdZamestnanec;
                    cmd.Parameters.Add("p_pobocka_id_pobocka", OracleDbType.Int32).Value = SelectedPobockaId;
                    cmd.Parameters.Add("p_prace_pozice_id_pozice", OracleDbType.Int32).Value = 1;
                    cmd.Parameters.Add("p_email_zamestnanec", OracleDbType.Varchar2).Value = Email;
                    OracleParameter idBankerParam = new OracleParameter("p_id_zamestnanec", OracleDbType.Int32);
                    idBankerParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idBankerParam);
                    cmd.ExecuteNonQuery();
                    if (cmd.Parameters["p_id_zamestnanec"].Value != DBNull.Value)
                    {
                        OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["p_id_zamestnanec"].Value;
                        idBanker = oracleDecimal.ToInt32();
                        Zamestnanec banker = new Zamestnanec
                        {
                            IdZamestnanec = idBanker,
                            Jmeno = Jmeno,
                            Prijmeni = Prijmeni,
                            Mzda = Mzda.HasValue ? (decimal)Mzda.Value : 0m,
                            TelefoniCislo = TelCislo,
                            AdresaIdAdres = _adresa.IdAdres,
                            BankIdBank = AsignManazer.BankIdBank,
                            StatusIdStatus = SelectedStatusId,
                            ZamestnanecIdZamestnanec1 = AsignManazer.IdZamestnanec,
                            PobockaIdPobocka = SelectedPobockaId,
                            PracePoziceIdPozice = 1,
                            EmailZamestnanec = Email,
                            OdesilaniFileIdFile = null,
                            OdesilaniFileIdKlient = null,
                            IdFile1 = null,
                            IdKlient1 = null                   
                        };
                        NovyBanker = banker;
                        InsertLoginData();
                        System.Windows.MessageBox.Show("Banker vytvořen!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }
        private string Validate(string columnName)
        {
            if (columnName == nameof(Email))
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return "Email nemuze byt prazdne";
                }

                if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    return "Nespravny format email";
                }
            }
            else if (columnName == nameof(TelCislo))
            {
                if (string.IsNullOrEmpty(TelCislo))
                {
                    return "Telefonni cislo nemuze byt prazdne";
                }

                if (!Regex.IsMatch(TelCislo, @"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}-\d{4}$"))
                {
                    return "Nespravny format telefonniho cisla";
                }
            }
            else if (columnName == nameof(Jmeno))
            {
                if (string.IsNullOrEmpty(Jmeno))
                {
                    return "Jmeno nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Prijmeni))
            {
                if (string.IsNullOrEmpty(Prijmeni))
                {
                    return "Prijmeni nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(PSC))
            {
                if (string.IsNullOrEmpty(PSC))
                {
                    return "Adresa: PSC nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Stat))
            {
                if (string.IsNullOrEmpty(Stat))
                {
                    return "Adresa: Stat nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Ulice))
            {
                if (string.IsNullOrEmpty(Ulice))
                {
                    return "Adresa: Ulice nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Mesto))
            {
                if (string.IsNullOrEmpty(Mesto))
                {
                    return "Adresa: Mesto nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(CisloPopisne))
            {
                if (string.IsNullOrEmpty(CisloPopisne))
                {
                    return "Adresa: CisloPopisne nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Mzda))
            {
                if (!_mzda.HasValue)
                {
                    return "Mzda nemuze byt prazdne";
                }
                if (_mzda < 20000 || _mzda > 80000)
                {
                    return "Mzda musi byt mezi 20000 a 80000";
                }
            }
            return null;
        }
        private bool _isDataValid;
        public bool IsDataValid
        {
            get { return _isDataValid; }
            set
            {
                if (_isDataValid != value)
                {
                    _isDataValid = value;
                    OnPropertyChanged(nameof(IsDataValid));
                }
            }
        }
        private void ValidateAll()
        {
            IsDataValid = string.IsNullOrEmpty(this["Email"]) &&
                          string.IsNullOrEmpty(this["TelCislo"]) &&
                          string.IsNullOrEmpty(this["Jmeno"]) &&
                          string.IsNullOrEmpty(this["Prijmeni"]) &&
                          string.IsNullOrEmpty(this["CisloPopisne"]) &&
                          string.IsNullOrEmpty(this["PSC"]) &&
                          string.IsNullOrEmpty(this["Stat"]) &&
                          string.IsNullOrEmpty(this["Ulice"]) &&
                          string.IsNullOrEmpty(this["Mesto"]) &&
                          _mzda.HasValue && _mzda >= 20000 && _mzda <= 80000;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidateAll();
        }
    }
}
