﻿using BCSH2_BDAS2_SemPrace.Commands;
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

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class UpraviPobockuViewModel : INotifyPropertyChanged
    {
        private readonly OracleDatabaseService db;
        private string _nazev;
        public string _telefoniCislo;
        private Pobocka aktualniPobocka;
        public ICommand UpravitCommand { get; }
        public int SelectedStatusId { get; set; }
        private int _idPobocka;
        public ObservableCollection<Status> Statusy { get; set; }
        private Pobocka pomocPobocka;
        
        public UpraviPobockuViewModel(Pobocka pobocka)
        {
            db = new OracleDatabaseService();
            db.OpenConnection();
            Statusy = new ObservableCollection<Status>();
            LoadStatusy();
            pomocPobocka = new Pobocka();
            pomocPobocka = pobocka;
            aktualniPobocka = new Pobocka();
            AktualniPobocka = pobocka;
            SelectedStatusId = AktualniPobocka.IdStatus;
            Nazev = AktualniPobocka.Nazev;
            TelefoniCislo = AktualniPobocka.TelefoniCislo;
            UpravitCommand = new RelayCommand(Upravit);
        }
        public string TelefoniCislo
        {
            get { return _telefoniCislo; }
            set
            {
                if (_telefoniCislo != value)
                {
                    _telefoniCislo = value;
                    OnPropertyChanged(nameof(TelefoniCislo));
                }
            }
        }
        public Pobocka AktualniPobocka
        {
            get => aktualniPobocka;
            set
            {
                aktualniPobocka = value;
                OnPropertyChanged(nameof(AktualniPobocka));
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
        public int IdPobocka
        {
            get => _idPobocka;
            set
            {
                _idPobocka = value;
                OnPropertyChanged(nameof(IdPobocka));
            }
        }
        private void Upravit(object obj)
        {
            
            try
            {   
                    db.OpenConnection();
                    using (OracleCommand command = new OracleCommand("UPDATE_POBOCKA", db.Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("p_id_pobocka", OracleDbType.Int32).Value = AktualniPobocka.IdPobocka;
                        command.Parameters.Add("p_adresa_id_adres", OracleDbType.Int32).Value = AktualniPobocka.IdAdresa;
                        command.Parameters.Add("p_bank_id_bank", OracleDbType.Int32).Value = AktualniPobocka.IdBank;
                        command.Parameters.Add("p_nazev", OracleDbType.Varchar2).Value = Nazev;
                        command.Parameters.Add("p_telefoni_cislo", OracleDbType.Varchar2).Value = TelefoniCislo;
                        command.Parameters.Add("p_status_id_status", OracleDbType.Int32).Value = SelectedStatusId;

                        command.ExecuteNonQuery();
                    }
                    MessageBoxResult result = MessageBox.Show("Pobocka byla upravena.", "Uspech", MessageBoxButton.OK, MessageBoxImage.Information);
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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
