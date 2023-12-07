using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using BCSH2_BDAS2_SemPrace.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class ShowKlientViewModel : INotifyPropertyChanged
    {
        private string _jmeno;
        private string _prijmeni;
        private long _cisloPrukazu;
        private string _klientEmail;
        private string _telCislo;
        private string _adresa;
        private string _idUcet;
        private string _cisloUctu;
        private string _nazevUcetu;
        private string _statusPopis;
        private Klient _klient;
        public List<Ucet> Ucty { get; set; }

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

        public long CisloPrukazu
        {
            get { return _cisloPrukazu; }
            set
            {
                _cisloPrukazu = value;
                OnPropertyChanged(nameof(CisloPrukazu));
            }
        }

        public string KlientEmail
        {
            get { return _klientEmail; }
            set
            {
                _klientEmail = value;
                OnPropertyChanged(nameof(KlientEmail));
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

        public string IdUcet
        {
            get { return _idUcet; }
            set
            {
                _idUcet = value;
                OnPropertyChanged(nameof(IdUcet));
            }
        }

        public string CisloUctu
        {
            get { return _cisloUctu; }
            set
            {
                _cisloUctu = value;
                OnPropertyChanged(nameof(CisloUctu));
            }
        }

        public string NazevUcetu
        {
            get { return _nazevUcetu; }
            set
            {
                _nazevUcetu = value;
                OnPropertyChanged(nameof(NazevUcetu));
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
        public Klient Klient
        {
            get { return _klient; }
            set
            {
                _klient = value;
                OnPropertyChanged(nameof(Klient));
            }
        }

        public ShowKlientViewModel(Klient klient)
        {
            Klient = klient;
            Jmeno = klient.Jmeno;
            Prijmeni = klient.Prijmeni;
            CisloPrukazu = klient.CisloPrukazu;
            KlientEmail = klient.KlientEmail;
            TelCislo = klient.TelefoniCislo;
            Adresa = klient.Adresa;
            if (Klient.Ucty != null && Klient.Ucty.Any())
            {

                Ucty = Klient.Ucty.Select(ucet => new Ucet
                {
                    IdUcet = ucet.IdUcet,
                    CisloUctu = ucet.CisloUctu,
                    Nazev = ucet.Nazev,
                    PopisStatus = ucet.PopisStatus
                }).ToList();
            }
            else
            {
                Ucty = new List<Ucet>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
