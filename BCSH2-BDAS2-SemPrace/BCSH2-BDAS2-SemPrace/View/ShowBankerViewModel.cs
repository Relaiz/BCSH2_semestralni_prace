using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.View
{
    public class ShowBankerViewModel : INotifyPropertyChanged
    {
        private string _jmeno;
        private string _prijmeni;
        private decimal _mzda;
        private string _telCislo;
        private string _bankerEmail;
        private string _nazevPobocka;
        private string _statusPopis;
        private string _adresa;
        private Zamestnanec _banker;
        public ShowBankerViewModel(Zamestnanec banker)
        {
            Jmeno=banker.Jmeno;
            Prijmeni = banker.Prijmeni;
            Mzda = banker.Mzda;
            TelCislo = banker.TelefoniCislo;
            BankerEmail = banker.EmailZamestnanec;
            NazevPobocky = banker.NazevPobocky;
            StatusPopis = banker.StatusPopis;
            Adresa = banker.Adresa;


        }
        public string Jmeno
        {
            get { return _jmeno; }
            set
            {
                _jmeno = value;
                OnPropertyChanged(nameof(Jmeno));
            }
        }

        public string Prijmeni
        {
            get { return _prijmeni; }
            set
            {
                _prijmeni = value;
                OnPropertyChanged(nameof(Prijmeni));
            }
        }
        public decimal Mzda
        {
            get { return _mzda; }
            set
            {
                _mzda = value;
                OnPropertyChanged(nameof(Mzda));
            }
        }
        public string NazevPobocky
        {
            get { return _nazevPobocka; }
            set
            {
                _nazevPobocka = value;
                OnPropertyChanged(nameof(NazevPobocky));
            }
        }
        public string StatusPopis
        {
            get { return _statusPopis; }
            set
            {
                _statusPopis = value;
                OnPropertyChanged(nameof(StatusPopis));
            }
        }
        public string BankerEmail
        {
            get { return _bankerEmail; }
            set
            {
                _bankerEmail = value;
                OnPropertyChanged(nameof(BankerEmail));
            }
        }
        public string TelCislo
        {
            get { return _telCislo; }
            set
            {
                _telCislo = value;
                OnPropertyChanged(nameof(TelCislo));
            }
        }
        public string Adresa
        {
            get { return _adresa; }
            set
            {
                _adresa = value;
                OnPropertyChanged(nameof(Adresa));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
