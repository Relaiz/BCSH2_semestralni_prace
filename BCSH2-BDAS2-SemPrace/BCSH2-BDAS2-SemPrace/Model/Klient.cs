using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Klient : INotifyPropertyChanged
    {
        private int _idKlient;
        private int _cisloPrukazu;
        private string _jmeno;
        private string _prijmeni;
        private string _klientEmail;
        private int _adresaIdAdres;
        private int _bankIdBank;
        private string _telefoniCislo;
        private int _zameIdZamestnanec;
        private int? _odesiFileIdFile;
        private int? _odesFileIdKlient;
        private int? _idFile1;
        private int? _idKlient2;

        
        public Klient SelectedKlient { get; set; }
        public ICommand EditKlientCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int IdKlient
        {
            get { return _idKlient; }
            set { _idKlient = value; OnPropertyChanged(nameof(IdKlient)); }
        }

        public int CisloPrukazu
        {
            get { return _cisloPrukazu; }
            set { _cisloPrukazu = value; OnPropertyChanged(nameof(CisloPrukazu)); }
        }

        public string Jmeno
        {
            get { return _jmeno; }
            set { _jmeno = value; OnPropertyChanged(nameof(Jmeno)); }
        }

        public string Prijmeni
        {
            get { return _prijmeni; }
            set { _prijmeni = value; OnPropertyChanged(nameof(Prijmeni)); }
        }

        public string KlientEmail
        {
            get { return _klientEmail; }
            set { _klientEmail = value; OnPropertyChanged(nameof(KlientEmail)); }
        }

        public int AdresaIdAdres
        {
            get { return _adresaIdAdres; }
            set { _adresaIdAdres = value; OnPropertyChanged(nameof(AdresaIdAdres)); }
        }

        public int BankIdBank
        {
            get { return _bankIdBank; }
            set { _bankIdBank = value; OnPropertyChanged(nameof(BankIdBank)); }
        }

        public string TelefoniCislo
        {
            get { return _telefoniCislo; }
            set { _telefoniCislo = value; OnPropertyChanged(nameof(TelefoniCislo)); }
        }

        public int ZameIdZamestnanec
        {
            get { return _zameIdZamestnanec; }
            set { _zameIdZamestnanec = value; OnPropertyChanged(nameof(ZameIdZamestnanec)); }
        }

        public int? OdesiFileIdFile
        {
            get { return _odesiFileIdFile; }
            set { _odesiFileIdFile = value; OnPropertyChanged(nameof(OdesiFileIdFile)); }
        }

        public int? OdesFileIdKlient
        {
            get { return _odesFileIdKlient; }
            set { _odesFileIdKlient = value; OnPropertyChanged(nameof(OdesFileIdKlient)); }
        }

        public int? IdFile1
        {
            get { return _idFile1; }
            set { _idFile1 = value; OnPropertyChanged(nameof(IdFile1)); }
        }

        public int? IdKlient2
        {
            get { return _idKlient2; }
            set { _idKlient2 = value; OnPropertyChanged(nameof(IdKlient2)); }
        }

        private string _adresa;

        public string Adresa
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
        // Add properties for other fields in the klient table

        // ... (rest of the class)

        public static ObservableCollection<Klient> ConvertDataTableToObservableCollection(DataTable dataTable)
        {
            ObservableCollection<Klient> klientList = new ObservableCollection<Klient>();

            foreach (DataRow row in dataTable.Rows)
            {
                Klient klient = new Klient
                {
                    IdKlient = Convert.ToInt32(row["id_klient"]),
                    CisloPrukazu = Convert.ToInt32(row["cislo_prukazu"]),
                    Jmeno = row["jmeno"].ToString(),
                    Prijmeni = row["prijmeni"].ToString(),
                    KlientEmail = row["klient_email"].ToString(),
                    AdresaIdAdres = Convert.ToInt32(row["adresa_id_adres"]),
                    BankIdBank = Convert.ToInt32(row["bank_id_bank"]),
                    TelefoniCislo = row["telefoni_cislo"].ToString(),
                    ZameIdZamestnanec = Convert.ToInt32(row["zame_id_zamestnanec"]),
                    OdesiFileIdFile = row["odesi_file_id_file"] as int?,
                    OdesFileIdKlient = row["odes_file_id_klient"] as int?,
                    IdFile1 = row["id_file1"] as int?,
                    IdKlient2 = row["id_klient2"] as int?
                    // Set other properties based on your Klient class
                };

                klientList.Add(klient);
            }

            return klientList;
        }
    }
}