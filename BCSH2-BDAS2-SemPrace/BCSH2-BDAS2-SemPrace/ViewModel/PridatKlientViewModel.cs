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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{


    public class PridatKlientViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        

        private string _email;
        private string _telCislo;
        private string _name;
        private string _lastname;
        private long _cisloPrukaz;
        private Adresa _adresa = new Adresa(); 
        private string _psc;
        private string _ulice;
        private string _stat;
        private string _mesto;
        private string _cisloPopisne;
        private string _heslo;
        private Klient _klient= new Klient();
        public event EventHandler KlientAdded;
        private ZamestnanecViewModel _zamestnanecViewModel;
        protected virtual void OnKlientAdded()
        {
            KlientAdded?.Invoke(this, EventArgs.Empty);
        }
        public ICommand CreateClientCommand { get; }
        
        private readonly OracleDatabaseService db;

        private Zamestnanec _asignZamestnanec;


        public PridatKlientViewModel()
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Heslo = "abcde";
            
            CreateClientCommand = new RelayCommand(PridatKlient);
        }
        public PridatKlientViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Heslo = "abcde";
            AsignZam = zamestnanec;
            CreateClientCommand = new RelayCommand(PridatKlient);
        }
        public PridatKlientViewModel(ZamestnanecViewModel ZamestnanecViewModel,Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            AsignZam = zamestnanec;
            Heslo = "abcde";
            CreateClientCommand = new RelayCommand(PridatKlient);
            _zamestnanecViewModel = ZamestnanecViewModel;
        }

        private Zamestnanec AsignZam
        {
            get { return _asignZamestnanec; }
            set
            {
                if (_asignZamestnanec != value)
                {
                    _asignZamestnanec = value;
                    OnPropertyChanged(nameof(AsignZam));
                }
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

                        // Check if the OracleDecimal is not Null
                        if (!oracleDecimal.IsNull)
                        {
                            // Convert OracleDecimal to Int32
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

                // Используем новую функцию для проверки существования адреса
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

                        // Check if the output parameter is DBNull
                        if (cmd.Parameters["id_adres"].Value != DBNull.Value)
                        {
                            OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["id_adres"].Value;

                            // Use ToInt32 method of OracleDecimal to convert to integer
                            id_adres = oracleDecimal.ToInt32();

                            // Initialize _adresa object
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

                using (OracleCommand cmd = new OracleCommand("AddKlientLogin", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Добавьте параметры и их значения в соответствии с вашими данными
                    cmd.Parameters.Add("p_zamestnanec_id_zamestnanec", OracleDbType.Int32).Value = null;
                    cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = _klient.KlientEmail;
                    cmd.Parameters.Add("p_heslo", OracleDbType.Varchar2).Value = Heslo;
                    cmd.Parameters.Add("p_is_admin", OracleDbType.Int32).Value = 0;
                    cmd.Parameters.Add("p_klient_id_klient", OracleDbType.Int32).Value = _klient.IdKlient;
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
        private void PridatKlient(object parameter)
        {
            CheckOrAddAdresa();

            try
            {
                db.OpenConnection();
                int idKlient = 0;
                using (OracleCommand cmd = new OracleCommand("AddKlient", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cislo_prukazu", OracleDbType.Int32).Value = CisloPrukaz;
                    cmd.Parameters.Add("p_jmeno", OracleDbType.Varchar2).Value = Name;
                    cmd.Parameters.Add("p_prijmeni", OracleDbType.Varchar2).Value = Lastname;
                    cmd.Parameters.Add("p_klient_email", OracleDbType.Varchar2).Value = Email;
                    cmd.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = _adresa.IdAdres;
                    cmd.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = AsignZam.BankIdBank;
                    cmd.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelCislo;
                    cmd.Parameters.Add("p_zame_id_zamestnanec", OracleDbType.Int32).Value = AsignZam.IdZamestnanec;
                    OracleParameter idKlientParam = new OracleParameter("p_id_klient", OracleDbType.Int32);
                    idKlientParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idKlientParam);

                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_id_klient"].Value != DBNull.Value)
                    {
                        OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["p_id_klient"].Value;

                        // Use ToInt32 method of OracleDecimal to convert to integer
                        idKlient = oracleDecimal.ToInt32();

                        // int idKlient = Convert.ToInt32(idKlientParam.Value);
                        Klient klient = new Klient
                        {
                            IdKlient = idKlient,
                            CisloPrukazu = CisloPrukaz,
                            Jmeno = Name,
                            Prijmeni = Lastname,
                            KlientEmail = Email,
                            AdresaIdAdres = _adresa.IdAdres,
                            BankIdBank = AsignZam.BankIdBank,
                            TelefoniCislo = TelCislo,
                            ZameIdZamestnanec = AsignZam.IdZamestnanec,
                            OdesFileIdKlient = null,
                            OdesiFileIdFile = null,
                            IdFile1 = null,
                            IdKlient2 = null

                        };
                        _klient = klient;
                        InsertLoginData();
                        Console.WriteLine("Success adding klient.");
                        MessageBoxResult result = MessageBox.Show("Success adding klient.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result == MessageBoxResult.OK)
                        {
                            // Закрываем текущее окно
                            Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                            if (actualnWindow != null)
                            {
                                // Закрываем текущее окно
                                actualnWindow.Close();
                                Window noveWindow = new KlientZalozitUcetWindow(_klient.IdKlient);
                                noveWindow.Show();
                                
                                _zamestnanecViewModel?.UpdateList();
                            }
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

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(Name);
                }
            }
        }
        public string Lastname
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(Lastname);
                }
            }
        }
        public long CisloPrukaz
        {
            get { return _cisloPrukaz; }
            set
            {
                if (_cisloPrukaz != value)
                {
                    _cisloPrukaz = value;
                    OnPropertyChanged(nameof(CisloPrukaz));
                }
            }
        }
        public Adresa Adresa
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
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                // Ваша логика валидации
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
            else if (columnName == nameof(Name))
            {
                if (string.IsNullOrEmpty(Name))
                {
                    return "Jmeno nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(Lastname))
            {
                if (string.IsNullOrEmpty(Lastname))
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
            
            else if (columnName == nameof(CisloPrukaz))
            {
                if (string.IsNullOrEmpty(CisloPrukaz.ToString()))
                {
                    return "CisloPrukaz nemuze byt prazdne";
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
                           string.IsNullOrEmpty(this["Name"]) &&
                           string.IsNullOrEmpty(this["Lastname"]) &&
                           string.IsNullOrEmpty(this["CisloPopisne"]) &&
                           string.IsNullOrEmpty(this["PSC"]) &&
                           string.IsNullOrEmpty(this["Stat"]) &&
                           string.IsNullOrEmpty(this["Ulice"]) &&
                           string.IsNullOrEmpty(this["Mesto"]) &&

                          CisloPrukaz !=0; 
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidateAll();
        }
    }


}
