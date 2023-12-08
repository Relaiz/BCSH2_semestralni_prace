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
using static System.Windows.Forms.AxHost;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using BCSH2_BDAS2_SemPrace.Commands;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class PridatBankomatViewModel : INotifyPropertyChanged, IDataErrorInfo
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
        public ICommand PridatBankomatCommand { get; }
        private Zamestnanec _zamestnanec;
        public PridatBankomatViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Admin = zamestnanec;
            PridatBankomatCommand = new RelayCommand(PridatBankomat);
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

        private void PridatBankomat(object parameter)
        {
            CheckOrAddAdresa();
            try
            {
                db.OpenConnection();
                int idBankomat = 0;
                using (OracleCommand cmd = new OracleCommand("AddBankomat", db.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                                    
                    cmd.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = _adresa.IdAdres;
                    cmd.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = Admin.BankIdBank;
                    cmd.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = Nazev;
                    cmd.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;
                    OracleParameter idBankomatParam = new OracleParameter("p_id_bankomat", OracleDbType.Int32);
                    idBankomatParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(idBankomatParam);
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_id_bankomat"].Value != DBNull.Value)
                    {
                        OracleDecimal oracleDecimal = (OracleDecimal)cmd.Parameters["p_id_bankomat"].Value;
                        idBankomat = oracleDecimal.ToInt32();
                        Bankomat bankomat = new Bankomat
                        {
                            IdBamkomat = idBankomat,
                            IdAdresa = _adresa.IdAdres,
                            Nazev = Nazev,
                            IdStatus = SelectedStatusId
                        };
                      
                        MessageBoxResult result = System.Windows.MessageBox.Show("Bankomat vytvořen!", "Úspěch", MessageBoxButton.OK, MessageBoxImage.Information);
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
