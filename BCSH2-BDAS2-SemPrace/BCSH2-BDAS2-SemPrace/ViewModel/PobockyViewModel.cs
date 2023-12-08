using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
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
using Oracle.ManagedDataAccess.Client;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class PobockyViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        public ObservableCollection<Pobocka> Pobocky { get; set; }
        public Pobocka selectedPobocka;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _IdBank;
        private int _bankIdbank;
        private int _idStatus;
        private int _idPobocka;
        private string _nazev;
        private string _popis;
        private string _telCislo;
        public int SelectedPobockaId { get; set; }
        public ICommand PridatPobockuCommand { get; private set; }
        public ICommand OdebratPobockuCommand { get; private set; }
        public ICommand UpravitPobockuCommand { get; private set; }
        public ICommand ZavritCommand { get; private set; }

        public Zamestnanec Admin { get; set; }
        public PobockyViewModel(Zamestnanec zamestnanec)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            
            Pobocky = new ObservableCollection<Pobocka>();
            LoadPobocky();
            selectedPobocka = new Pobocka();
            Admin = new Zamestnanec();
            Admin = zamestnanec;
            PridatPobockuCommand = new RelayCommand(PridatPobocku);
            OdebratPobockuCommand = new RelayCommand(OdebratPobocku);
            UpravitPobockuCommand = new RelayCommand(UpravitPobocku);
            ZavritCommand = new RelayCommand(Zavrit);
        }

        private void Zavrit(object obj)
        {
            Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (actualnWindow != null)
            {
                actualnWindow.Close();
            }
        }
        public string TelefoniCislo
        {
            get => _telCislo;
            set
            {
                _telCislo = value;
                OnPropertyChanged(nameof(TelefoniCislo));
            }
        }
        private void UpravitPobocku(object obj)
        {
            if (AktualniPobocka != null)
            {
                UpravitPobockuWindow upravitPobockuWindow = new UpravitPobockuWindow(AktualniPobocka);
                UpraviPobockuViewModel upraviPobockuViewModel = new UpraviPobockuViewModel(AktualniPobocka);
                upravitPobockuWindow.DataContext = upraviPobockuViewModel;
                upravitPobockuWindow.ShowDialog();
                Pobocky.Clear();
                LoadPobocky();
            }

        }

        private void OdebratPobocku(object obj)
        {
            if (AktualniPobocka == null)
            {
                System.Windows.MessageBox.Show("Nejprve vyberte pobocku ze seznamu.", "Upozorneni", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    db.OpenConnection();
                    using (OracleCommand command = new OracleCommand("DeletePobocka", db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_id_pobocka", OracleDbType.Int32).Value = AktualniPobocka.IdPobocka;

                        command.ExecuteNonQuery();

                    }
                    Pobocky.Clear();
                    LoadPobocky();
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
        public int IdPobocka
        {
            get => _idPobocka;
            set
            {
                _idPobocka = value;
                OnPropertyChanged(nameof(IdPobocka));
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
        public Pobocka AktualniPobocka
        {
            get => selectedPobocka;
            set
            {
                selectedPobocka = value;
                OnPropertyChanged(nameof(AktualniPobocka));
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LoadPobocky()
        {
            try
            {
                db.OpenConnection();
                string query = "SELECT id_pobocka, nazev,telefoni_cislo,id_status, popis,ADRESA, id_adres,bank_id_bank FROM pobocka_details";

                using (OracleCommand cmd = new OracleCommand(query, db.Connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pobocka pobocka = new Pobocka
                            {
                                IdPobocka = Convert.ToInt32(reader["id_pobocka"]),
                                Nazev = reader["nazev"].ToString(),
                                TelefoniCislo = reader["telefoni_cislo"].ToString(),
                                IdStatus = Convert.ToInt32(reader["id_status"]),
                                StatusPopis = reader["popis"].ToString(),
                                Adresa = reader["ADRESA"].ToString(),
                                IdAdresa= Convert.ToInt32(reader["id_adres"]),
                                IdBank = Convert.ToInt32(reader["bank_id_bank"])
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

        private void PridatPobocku(object parametr)
        {
            PridatPobockuWindow pridatPobockuWindow = new PridatPobockuWindow(Admin);
            PridatPobockuViewModel pridatPobockuViewModel = new PridatPobockuViewModel(Admin);
            pridatPobockuWindow.DataContext = pridatPobockuViewModel;
            pridatPobockuWindow.ShowDialog();
            Pobocky.Clear();
            LoadPobocky();
        }
    }
}
