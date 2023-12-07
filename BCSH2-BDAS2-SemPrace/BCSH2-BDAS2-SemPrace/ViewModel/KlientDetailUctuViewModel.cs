using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Oracle.ManagedDataAccess.Types;
using System.Xml.Linq;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientDetailUctuViewModel : ViewModelBase
    {


        private string nazevUctu;
        public string NazevUctu
        {
            get { return nazevUctu; }
            set
            {
                nazevUctu = value;
                OnPropertyChanged(nameof(NazevUctu));
            }
        }

        private long cisloKarty;
        public long CisloKarty
        {
            get { return cisloKarty; }
            set
            {
                cisloKarty = value;
                OnPropertyChanged(nameof(CisloKarty));
            }
        }

        private decimal _volnaCastka;
        public decimal VolnaCastka
        {
            get { return _volnaCastka; }
            set
            {
                _volnaCastka = value;
                OnPropertyChanged(nameof(VolnaCastka));
            }
        }

        private decimal _blokovanaCastka;
        public decimal BlokovanaCastka
        {
            get { return _blokovanaCastka; }
            set
            {
                _blokovanaCastka = value;
                OnPropertyChanged(nameof(BlokovanaCastka));
            }
        }


        private string typ;
        public string Typ
        {
            get { return typ; }
            set
            {
                typ = value;
                OnPropertyChanged(nameof(Typ));
            }
        }

        private string platebniSystem;
        public string PlatebniSystem
        {
            get { return platebniSystem; }
            set
            {
                platebniSystem = value;
                OnPropertyChanged(nameof(PlatebniSystem));
            }
        }

        private DateTime platnost;
        public DateTime Platnost
        {
            get { return platnost; }
            set { platnost = value; OnPropertyChanged(nameof(Platnost)); }
        }

        private long cisloUctu;
        public long CisloUctu
        {
            get { return cisloUctu; }
            set
            {
                cisloUctu = value;
                OnPropertyChanged(nameof(CisloUctu));
            }
        }

        private Karta _selectedKarta;
        public Karta SelectedKarta
        {
            get { return _selectedKarta; }
            set
            {
                _selectedKarta = value;
                OnPropertyChanged(nameof(SelectedKarta));
            }
        }


        private ObservableCollection<Karta> _listKaret;
        public ObservableCollection<Karta> ListKaret
        {
            get { return _listKaret; }
            set
            {
                _listKaret = value;
                OnPropertyChanged(nameof(ListKaret));
            }
        }
        public ICommand ObjednatKartuCommand { get; }
        public ICommand ZavritOknoCommand { get; }
        public ICommand ZablokovatKartuCommand { get; }
        public ICommand SaveUcetNameCommand { get; }

        private Ucet _currentUcet;
        private Klient _currentKlient;
        private readonly OracleDatabaseService db;

        public KlientDetailUctuViewModel(Ucet ucet, Klient klient)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            _currentUcet = ucet;
            _currentKlient = klient;
            ListKaret = new ObservableCollection<Karta>(GetKartyForUcet(_currentUcet.IdUcet));
            InitializeLabels();

            ObjednatKartuCommand = new RelayCommand(ObjednatKartu);
            ZavritOknoCommand = new RelayCommand(ZavritOkno);
            ZablokovatKartuCommand = new RelayCommand(ZablokovatKartu);
            SaveUcetNameCommand = new RelayCommand(SaveUcetName);
        }

        private void InitializeLabels()
        {
            NazevUctu = _currentUcet.Nazev;
            CisloUctu = _currentUcet.CisloUctu;
            LoadZustatekData();
        }

        private void ObjednatKartu(object parameter)
        {
            // Check if the selected Ucet is not null
            if (_currentUcet != null)
            {
                KlientObjednaniKartyWindow objednaniKartyWindow = new KlientObjednaniKartyWindow(_currentUcet, _currentKlient);          

                // Show the window as a dialog
                objednaniKartyWindow.ShowDialog();
                UpdateListKaret();
            }            
        }

        private void ZavritOkno(object parameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }

        private void LoadZustatekData()
        {
            // Fetch and set Zustatek and BlockedZustatek for the current Ucet
            FetchZustatekForUcet(_currentUcet.IdUcet, out decimal zustatek, out decimal blockedZustatek);
            VolnaCastka = zustatek;
            BlokovanaCastka = blockedZustatek;

            // Notify property changed for the UI to update
            OnPropertyChanged(nameof(VolnaCastka));
            OnPropertyChanged(nameof(BlokovanaCastka));
        }

        // Fetch Zustatek for the Ucet
        private void FetchZustatekForUcet(int ucetId, out decimal volnaCastka, out decimal blockedCastka)
        {
            // Call the GetZustatekForUcet function from OracleDatabaseService
            Zustatek zustatek = db.GetZustatekForUcet(ucetId);

            // Initialize output variables
            volnaCastka = 0;
            blockedCastka = 0;

            // Check if Zustatek is not null
            if (zustatek != null)
            {
                // Set the output variables
                volnaCastka = zustatek.VolnaCastka;
                blockedCastka = zustatek.BlokovaneCastka;
            }
        }

        private void ZablokovatKartu(object parameter)
        {
            if (SelectedKarta != null)
            {
                MessageBoxResult result = MessageBox.Show("Jste se jiste, ze chcete zablokovat tuto kartu?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    db.OpenConnection();
                    try
                    {
                        OracleCommand cmd = db.Connection.CreateCommand();
                        cmd.CommandText = "DELETE FROM karta WHERE id_karta = :idKarta";
                        cmd.Parameters.Add("idKarta", OracleDbType.Int32).Value = SelectedKarta.IdKarta;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Card successfully blocked
                            MessageBox.Show("Karta uspesne zablokovana.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Optionally, refresh the list of cards
                            UpdateListKaret();
                            db.CloseConnection();
                        }
                        else
                        {
                            MessageBox.Show("Nepodarilo se zablokovat kartu.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Chyba blokovani: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private List<Karta> GetKartyForUcet(int ucetId)
        {
            List<Karta> karty = new List<Karta>();
            db.OpenConnection();
            try
            {
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "GetKartyForUcet";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_ucet_id", OracleDbType.Int32).Value = ucetId;
                cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                // Execute the procedure
                cmd.ExecuteNonQuery();

                // Retrieve Karty information from the output cursor
                OracleRefCursor refCursor = (OracleRefCursor)cmd.Parameters["p_cursor"].Value;
                OracleDataReader reader = refCursor.GetDataReader();

                // Populate the list with Karty items
                while (reader.Read())
                {
                    Karta karta = new Karta
                    {
                        IdKarta = Convert.ToInt32(reader["id_karta"]),
                        Jmeno = reader["jmeno"].ToString(),
                        Prijmeni = reader["prijmeni"].ToString(),
                        CisloKarty = Convert.ToInt64(reader["cislo_karty"]),
                        PlatebniSystem = reader["platebni_system"].ToString(),
                        Platnost = Convert.ToDateTime(reader["platnost"]),
                        Typ = reader["typ"].ToString(),
                        UcetIdUcet = Convert.ToInt32(reader["ucet_id_ucet"])
                    };

                    karty.Add(karta);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, throw)
                Console.WriteLine($"Chyba dostanuti kart k uctu: {ex.Message}");
            }
            db.CloseConnection();
            return karty;
        }

        private void SaveUcetName(object parameter)
        {
            db.OpenConnection();
            try
            {
                // Call the PL/SQL procedure to update Klient data
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "UpdateNazevUctu";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("p_id_ucet", OracleDbType.Int32).Value = _currentUcet.IdUcet;
                cmd.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = NazevUctu;


                cmd.ExecuteNonQuery();

                // Refresh data after saving                
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba ulozeni dat: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }


        private void UpdateListKaret()
        {
            List<Karta> karty = GetKartyForUcet(_currentUcet.IdUcet);
            LoadZustatekData();

            // Clear the existing items
            ListKaret.Clear();

            // Add new items using foreach
            foreach (var karta in karty)
            {
                ListKaret.Add(karta);
            }
            OnPropertyChanged(nameof(ListKaret));
        }

    }
}
