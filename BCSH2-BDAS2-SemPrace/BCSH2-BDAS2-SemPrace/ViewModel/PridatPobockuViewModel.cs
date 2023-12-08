using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    internal class PridatPobockuViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly OracleDatabaseService db;
        private Adresa _adresa = new Adresa();
        public int SelectedStatusId { get; set; }
        private bool _isDataValid;
        private string _stat;
        private string _mesto;
        private string _ulice;
        private string _cisloPopisne;
        private string _psc;
        private string _nazev;
        public ObservableCollection<Status> Statusy { get; set; }
        public ICommand PridatPobockuCommand { get; }
        private Zamestnanec _zamestnanec;
        private string _telCislo;

        public PridatPobockuViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Admin = zamestnanec;
            PridatPobockuCommand = new RelayCommand(PridatPobocku);
            Statusy = new ObservableCollection<Status>();
            LoadStatusy();
        }
        public Zamestnanec Admin
        {
            get { return _zamestnanec; }
            set
            {
                if (_zamestnanec != value)
                {
                    _zamestnanec = value;
                    OnPropertyChanged(nameof(Admin));
                }
            }
        }
        public string Nazev
        {
            get { return _nazev; }
            set
            {
                if (_nazev != value)
                {
                    _nazev = value;
                    OnPropertyChanged(Nazev);
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
        public string TelefoniCislo
        {
            get { return _telCislo; }
            set
            {
                if (_telCislo != value)
                {
                    _telCislo = value;
                    OnPropertyChanged(TelefoniCislo);
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

        private void PridatPobocku(object parameter)
        {
            CheckOrAddAdresa();
            try
            {
                db.OpenConnection();
                int idPobocka = 0;
                using (OracleCommand cmd = new OracleCommand("AddPobocka", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    cmd.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = Nazev;
                    cmd.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = _adresa.IdAdres;
                    cmd.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = Admin.BankIdBank;
                    cmd.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelefoniCislo;
                    cmd.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;
                    cmd.Parameters.Add("p_zamestnanec_id_zamestnanec", OracleDbType.Int32).Value = Admin.IdZamestnanec;
                    cmd.Parameters.Add("p_id_file", OracleDbType.Int32).Value = null;
                    cmd.Parameters.Add("p_id_klient", OracleDbType.Int32).Value = null;

                    OracleParameter idPobockaParam = new OracleParameter("p_id_pobocka", OracleDbType.Int32);
                    idPobockaParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idPobockaParam);

                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_id_pobocka"].Value != DBNull.Value)
                    {
                        OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["p_id_pobocka"].Value;
                        idPobocka = oracleDecimal.ToInt32();

                        Pobocka pobocka = new Pobocka
                        {
                            IdPobocka = idPobocka,
                            IdAdresa = _adresa.IdAdres,
                            Nazev = Nazev,
                            IdStatus = SelectedStatusId,
                            TelefoniCislo = TelefoniCislo,
                            IdBank = Admin.BankIdBank,
                            
                        };

                        MessageBoxResult result = MessageBox.Show("Pobocka vytvořena!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (result == MessageBoxResult.OK)
                        {
                            Window actualnWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                            if (actualnWindow != null)
                            {
                                actualnWindow.Close();
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

            if (columnName == nameof(PSC))
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
            else if (columnName == nameof(Nazev))
            {
                if (string.IsNullOrEmpty(Nazev))
                {
                    return "Nazev nemuze byt prazdne";
                }
            }
            else if (columnName == nameof(TelefoniCislo))
            {
                if (string.IsNullOrEmpty(TelefoniCislo))
                {
                    return "Telefonni cislo nemuze byt prazdne";
                }

                if (!Regex.IsMatch(TelefoniCislo, @"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}-\d{4}$"))
                {
                    return "Nespravny format telefonniho cisla";
                }
            }

            return null;
        }


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
            IsDataValid = string.IsNullOrEmpty(this["Nazev"]) &&
                          string.IsNullOrEmpty(this["PSC"]) &&
                          string.IsNullOrEmpty(this["Stat"]) &&
                          string.IsNullOrEmpty(this["Ulice"]) &&
                          string.IsNullOrEmpty(this["TelefoniCislo"]) &&
                          string.IsNullOrEmpty(this["Mesto"]);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidateAll();
        }
    }
}
