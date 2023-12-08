using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientOdesilaniSouboruViewModel : ViewModelBase
    {
        private OracleDatabaseService db;

        private ObservableCollection<SouborOdesilani> _files;
        private SouborOdesilani _selectedFile;
        private Klient _currentKlient;

        public ObservableCollection<SouborOdesilani> Files
        {
            get { return _files; }
            set { _files = value; OnPropertyChanged(nameof(Files)); }
        }

        public SouborOdesilani SelectedFile
        {
            get { return _selectedFile; }
            set { _selectedFile = value; OnPropertyChanged(nameof(SelectedFile)); }
        }

        public ICommand UploadFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand DeleteFileCommand { get; }
        public ICommand EditFileCommand { get; }

        public KlientOdesilaniSouboruViewModel(Klient klient)
        {
            db = new OracleDatabaseService();
            Files = new ObservableCollection<SouborOdesilani>();
            _currentKlient = klient;

            UploadFileCommand = new RelayCommand(UploadFile);
            OpenFileCommand = new RelayCommand(OpenFile);
            DeleteFileCommand = new RelayCommand(DeleteFile);
            EditFileCommand = new RelayCommand(EditFile);

            LoadFiles();
        }

        private void LoadFiles()
        {
            db.OpenConnection();
            try
            {
                OracleCommand cmd = db.Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM ODESILANI_FILE WHERE Klient_Id_Klient = :klientId";
                cmd.Parameters.Add("klientId", OracleDbType.Int32).Value = _currentKlient.IdKlient;

                OracleDataReader reader = cmd.ExecuteReader();

                Files.Clear();

                while (reader.Read())
                {
                    SouborOdesilani file = new SouborOdesilani
                    {
                        IdFile = reader.GetInt32(reader.GetOrdinal("Id_File")),
                        NazevFile = reader.GetString(reader.GetOrdinal("Nazev_File")),
                        PrijemceFile = reader.GetString(reader.GetOrdinal("Prijemce_File")),
                        FormatFile = reader.GetString(reader.GetOrdinal("Format_File")),
                        ZameIdZamestnanec = reader.GetInt32(reader.GetOrdinal("Zamestnanec_Id_Zamestnanec")),
                        KlientIdKlient = reader.GetInt32(reader.GetOrdinal("Klient_Id_Klient"))
                    };

                    Files.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading files: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void UploadFile(object parameter)
        {
            db.OpenConnection();
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();                
                openFileDialog.Title = "Select a file to upload";
                openFileDialog.Filter = "All Files|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;

                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    OracleCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "UploadFile";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_nazev_file", OracleDbType.Varchar2).Value = Path.GetFileName(filePath);
                    cmd.Parameters.Add("p_file", OracleDbType.Blob).Value = fileBytes;
                    cmd.Parameters.Add("p_prijemce_file", OracleDbType.Varchar2).Value = "ReceiverName"; // Replace with actual logic for determining receiver name
                    cmd.Parameters.Add("p_format_file", OracleDbType.Varchar2).Value = Path.GetExtension(filePath);
                    cmd.Parameters.Add("p_zame_id_zamestnanec", OracleDbType.Int32).Value = _currentKlient.ZameIdZamestnanec; // Assuming you have a property for ZameIdZamestnanec
                    cmd.Parameters.Add("p_klient_id_klient", OracleDbType.Int32).Value = _currentKlient.IdKlient; // Assuming you have a property for KlientIdKlient

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("File uploaded successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the ObservableCollection
                    LoadFiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }


        private void OpenFile(object parameter)
        {
            db.OpenConnection();
            try
            {
                if (SelectedFile != null)
                {
                    OracleCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "SELECT GetFileBlob(:idFile) FROM DUAL";
                    cmd.Parameters.Add("idFile", OracleDbType.Int32).Value = SelectedFile.IdFile;

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        OracleBlob oracleBlob = reader.GetOracleBlob(0);
                        byte[] fileBytes = new byte[oracleBlob.Length];
                        oracleBlob.Read(fileBytes, 0, (int)oracleBlob.Length);

                        // Save the BLOB content to a temporary file
                        string tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}{Path.GetExtension(SelectedFile.NazevFile)}");
                        File.WriteAllBytes(tempFilePath, fileBytes);

                        // Open the file with the default associated application
                        System.Diagnostics.Process.Start(tempFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }


        private void DeleteFile(object parameter)
        {
            db.OpenConnection();
            try
            {
                if (SelectedFile != null)
                {
                    OracleCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "DeleteFile";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id_file", OracleDbType.Int32).Value = SelectedFile.IdFile;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("File deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the ObservableCollection
                    LoadFiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        private void EditFile(object parameter)
        {
            db.OpenConnection();
            try
            {
                // Use OpenFileDialog to select a new file for editing
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "All Files|*.*";
                openFileDialog.Title = "Select a file for editing";

                if (openFileDialog.ShowDialog() == true)
                {
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);

                    OracleCommand cmd = db.Connection.CreateCommand();
                    cmd.CommandText = "EditFile";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_id_file", OracleDbType.Int32).Value = SelectedFile.IdFile;
                    cmd.Parameters.Add("p_nazev_file", OracleDbType.Varchar2).Value = Path.GetFileName(openFileDialog.FileName);
                    cmd.Parameters.Add("p_file", OracleDbType.Blob).Value = fileBytes;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("File edited successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadFiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                db.CloseConnection();
            }
        }

    }
}
