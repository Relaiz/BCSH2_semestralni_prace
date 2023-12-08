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
    public class BankomatViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        public ObservableCollection<Bankomat> listBankomatu { get; set; }
        public Bankomat selectedBankomat;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _IdBank;
        private int _bankIdbank;
        private int _idStatus;
        private string _nazev;
        private string _popis;
        public ICommand PridatBankomatCommand { get; private set; }
        public ICommand OdebratBankomaCommand { get; private set; }
        public ICommand UpravitBankomaCommand { get; private set; }
        public Zamestnanec Admin { get; set; }
        public BankomatViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            listBankomatu = new ObservableCollection<Bankomat>();
            LoadBankomaty();
            selectedBankomat = new Bankomat();
            Admin = new Zamestnanec();
            Admin = zamestnanec;
            PridatBankomatCommand = new RelayCommand(PridatBankomat);
            OdebratBankomaCommand = new RelayCommand(OdebratBankomat);
        }

        private void OdebratBankomat(object obj)
        {
            if (AktualniBankomat == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte bankomat ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    db.OpenConnection();
                    using (OracleCommand command = new OracleCommand("DeleteBankomat", db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_jmeno", OracleDbType.Int32).Value = AktualniBankomat.IdBamkomat;
                        
                        command.ExecuteNonQuery();

                    }
                    listBankomatu.Clear();
                    LoadBankomaty();
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

        public int BankId
        {
            get => _bankIdbank;
            set
            {
                _bankIdbank = value;
                OnPropertyChanged(nameof(BankId));
            }
        }
        public int IdStatus
        {
            get => _idStatus;
            set
            {
                _idStatus = value;
                OnPropertyChanged(nameof(IdStatus));
            }
        }
        public string Popis
        {
            get => _popis;
            set
            {
                _popis = value;
                OnPropertyChanged(nameof(Popis));
            }
        }
        public string Nazev
        {
            get => _nazev;
            set
            {
                _nazev = value;
                OnPropertyChanged(nameof(Nazev));
            }
        }
        public int IdBankomat
        {
            get => _IdBank;
            set
            {
                _IdBank = value;
                OnPropertyChanged(nameof(IdBankomat));
            }
        }
        public Bankomat AktualniBankomat
        {
            get => selectedBankomat;
            set
            {
                selectedBankomat = value;
                OnPropertyChanged(nameof(AktualniBankomat));
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LoadBankomaty()
        {
            try
            {
                db.OpenConnection();
                Bankomat bankomat = null;
                string query = "SELECT id_bankomat,nazev,bank_id_bank,id_status,popis, adresa FROM bankomat_details";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bankomat = new Bankomat
                            {
                                IdBamkomat = Convert.ToInt32(reader["id_bankomat"]),
                                Nazev = reader["nazev"].ToString(),
                                IdBank = Convert.ToInt32(reader["bank_id_bank"]),
                                IdStatus = Convert.ToInt32(reader["id_status"]),
                                StatusPopis = reader["popis"].ToString(),
                                Adresa = reader["adresa"].ToString()
                            };
                            listBankomatu.Add(bankomat);
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

        private void PridatBankomat(object parametr)
        {
            PridatBankomatWindow pridatBankomatWindow = new PridatBankomatWindow(Admin);
            PridatBankomatViewModel pridatBankomatViewModel = new PridatBankomatViewModel(Admin);
            pridatBankomatWindow.DataContext = pridatBankomatViewModel;
            pridatBankomatWindow.ShowDialog();
            listBankomatu.Clear();
            LoadBankomaty();
        }
    }
}
