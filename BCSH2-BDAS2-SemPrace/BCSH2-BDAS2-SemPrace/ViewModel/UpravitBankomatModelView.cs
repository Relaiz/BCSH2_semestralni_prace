using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class UpravitBankomatModelView : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        private string _nazev;
        private Bankomat aktualniBamkomat;
        public ICommand UpravitCommand { get; }
        public int SelectedStatusId { get; set; }
        public ObservableCollection<Status> Statusy { get; set; }
        private Bankomat pomocBankomat;
        public UpravitBankomatModelView(Bankomat bankomat)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Statusy = new ObservableCollection<Status>();
            LoadStatusy();
            pomocBankomat = new Bankomat();
            pomocBankomat = bankomat;
            aktualniBamkomat = new Bankomat();
            AktualniBankomat = bankomat;
            SelectedStatusId = AktualniBankomat.IdStatus;
            Nazev = AktualniBankomat.Nazev;
            UpravitCommand = new RelayCommand(Upravit);
        }

        private void Upravit(object obj)
        {
            
                try
                {
                    db.OpenConnection();             
                    using (OracleCommand command = new OracleCommand("UPDATE_BANKOMAT", db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_id_bankomat ", OracleDbType.Int32).Value = pomocBankomat.IdBamkomat;
                        command.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = pomocBankomat.IdAdresa;
                        command.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = pomocBankomat.IdBank;
                        command.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = Nazev;
                        command.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;
                        
                        command.ExecuteNonQuery();
                    }
                    MessageBoxResult result = MessageBox.Show("Bankomat byl upraven.", "Uspech", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        Window actualnWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                        if (actualnWindow != null)
                        {
                            actualnWindow.Close();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Bankomat AktualniBankomat
        {
            get => aktualniBamkomat;
            set
            {
                aktualniBamkomat = value;
                OnPropertyChanged(nameof(AktualniBankomat));
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
    }
}
