using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Zamestnanec  : INotifyPropertyChanged
    {
        private int _idZamestnanec;
        private string _jmeno;
        private string _prijmeni;
        private decimal _mzda;
        private string _telefoniCislo;
        private int _adresaIdAdres;
        private int _bankIdBank;
        private int _statusIdStatus;
        private int _zamestnanecIdZamestnanec1;
        private int? _pobockaIdPobocka;
        private int _pracePoziceIdPozice;
        private string _emailZamestnanec;
        private int? _odesilaniFileIdFile;
        private int? _odesilaniFileIdKlient;
        private int? _idFile1;
        private int? _idKlient1;

        public ObservableCollection<Klient> ManagedZamestnancy { get; set; }
        public Zamestnanec SelectedZamestnanec { get; set; }
        public ICommand EditZamestnanecCommand { get; set; }

        public int IdZamestnanec
        {
            get { return _idZamestnanec; }
            set { _idZamestnanec = value; OnPropertyChanged(nameof(IdZamestnanec)); }
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

        public decimal Mzda
        {
            get { return _mzda; }
            set { _mzda = value; OnPropertyChanged(nameof(Mzda)); }
        }

        public string TelefoniCislo
        {
            get { return _telefoniCislo; }
            set { _telefoniCislo = value; OnPropertyChanged(nameof(TelefoniCislo)); }
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

        public int StatusIdStatus
        {
            get { return _statusIdStatus; }
            set { _statusIdStatus = value; OnPropertyChanged(nameof(StatusIdStatus)); }
        }

        public int ZamestnanecIdZamestnanec1
        {
            get { return _zamestnanecIdZamestnanec1; }
            set { _zamestnanecIdZamestnanec1 = value; OnPropertyChanged(nameof(ZamestnanecIdZamestnanec1)); }
        }

        public int? PobockaIdPobocka
        {
            get { return _pobockaIdPobocka; }
            set { _pobockaIdPobocka = value; OnPropertyChanged(nameof(PobockaIdPobocka)); }
        }

        public int PracePoziceIdPozice
        {
            get { return _pracePoziceIdPozice; }
            set { _pracePoziceIdPozice = value; OnPropertyChanged(nameof(PracePoziceIdPozice)); }
        }

        public string EmailZamestnanec
        {
            get { return _emailZamestnanec; }
            set { _emailZamestnanec = value; OnPropertyChanged(nameof(EmailZamestnanec)); }
        }

        public int? OdesilaniFileIdFile
        {
            get { return _odesilaniFileIdFile; }
            set { _odesilaniFileIdFile = value; OnPropertyChanged(nameof(OdesilaniFileIdFile)); }
        }

        public int? OdesilaniFileIdKlient
        {
            get { return _odesilaniFileIdKlient; }
            set { _odesilaniFileIdKlient = value; OnPropertyChanged(nameof(OdesilaniFileIdKlient)); }
        }

        public int? IdFile1
        {
            get { return _idFile1; }
            set { _idFile1 = value; OnPropertyChanged(nameof(IdFile1)); }
        }

        public int? IdKlient1
        {
            get { return _idKlient1; }
            set { _idKlient1 = value; OnPropertyChanged(nameof(IdKlient1)); }
        }

        // Добавьте свойства для остальных полей таблицы zamestnanec

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static ObservableCollection<Zamestnanec> ConvertDataTableToObservableCollection(DataTable dataTable)
        {
            ObservableCollection<Zamestnanec> zamestnanciList = new ObservableCollection<Zamestnanec>();

            foreach (DataRow row in dataTable.Rows)
            {
                Zamestnanec zamestnanec = new Zamestnanec
                {
                    IdZamestnanec = Convert.ToInt32(row["id_zamestnanec"]),
                    Jmeno = row["jmeno"].ToString(),
                    Prijmeni = row["prijmeni"].ToString(),
                    Mzda = Convert.ToDecimal(row["mzda"]),
                    TelefoniCislo = row["telefoni_cislo"].ToString(),
                    AdresaIdAdres = Convert.ToInt32(row["adresa_id_adres"]),
                    BankIdBank = Convert.ToInt32(row["bank_id_bank"]),
                    StatusIdStatus = Convert.ToInt32(row["status_id_status"]),
                    ZamestnanecIdZamestnanec1 = Convert.ToInt32(row["zamestnanec_id_zamestnanec1"]),
                    PobockaIdPobocka = row["pobocka_id_pobocka"] as int?,
                    PracePoziceIdPozice = Convert.ToInt32(row["prace_pozice_id_pozice"]),
                    EmailZamestnanec = row["email_zamestnanec"].ToString(),
                    OdesilaniFileIdFile = row["odesilani_file_id_file"] as int?,
                    OdesilaniFileIdKlient = row["odesilani_file_id_klient"] as int?,
                    IdFile1 = row["id_file1"] as int?,
                    IdKlient1 = row["id_klient1"] as int?
                    // Продолжайте для других свойств
                };

                zamestnanciList.Add(zamestnanec);
            }

            return zamestnanciList;
        }

    }
}