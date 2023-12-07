using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientTranzakceZUctuViewModel : ViewModelBase
    {
        private readonly OracleDatabaseService db;
        private readonly Ucet _selectedUcet;

        private ObservableCollection<Operace> _operaceList;

        public ObservableCollection<Operace> OperaceList
        {
            get { return _operaceList; }
            set
            {
                if (_operaceList != value)
                {
                    _operaceList = value;
                    OnPropertyChanged(nameof(OperaceList));
                }
            }
        }        

        public KlientTranzakceZUctuViewModel(Ucet selectedUcet)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            _selectedUcet = selectedUcet;
            OperaceList = new ObservableCollection<Operace>();
            LoadOperaceData();
            ZavritOknoCommand = new RelayCommand(ZavritOkno);
        }

        public ICommand ZavritOknoCommand { get; }

        private void LoadOperaceData()
        {
            try
            {
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM operace_view WHERE ucet_id_ucet = :ucetId";
                cmd.Parameters.Add("ucetId", OracleDbType.Int32).Value = _selectedUcet.IdUcet;

                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Operace operace = new Operace
                    {
                        Castka = reader.GetDecimal(reader.GetOrdinal("castka")),
                        DatumZacatka = reader.GetDateTime(reader.GetOrdinal("datum_zacatka")),
                        DatumOkonceni = reader.GetDateTime(reader.GetOrdinal("datum_okonceni")),
                        Nazev = reader.GetString(reader.GetOrdinal("nazev")),
                        ZUctu = reader.GetInt64(reader.GetOrdinal("z_uctu_cislo")),
                        DoUctu = reader.GetInt64(reader.GetOrdinal("do_uctu_cislo")),
                        IdUcet = reader.GetInt32(reader.GetOrdinal("ucet_id_ucet")),
                        PopisStatusu = reader.GetString(reader.GetOrdinal("status_popis")),
                    };

                    OperaceList.Add(operace);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading operations data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }        

        private void ZavritOkno(object parameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }
    }
}
