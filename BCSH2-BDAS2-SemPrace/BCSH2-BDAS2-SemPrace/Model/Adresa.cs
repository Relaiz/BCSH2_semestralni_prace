using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Adresa : INotifyPropertyChanged
    {
        private int _idAdres;
        private string _ulice;
        private string _mesto;
        private string _cisloPopisne;
        private string _psc;
        private string _stat;
        

        public int IdAdres
        {
            get { return _idAdres; }
            set
            {
                if (_idAdres != value)
                {
                    _idAdres = value;
                    OnPropertyChanged(nameof(IdAdres));
                }
            }
        }

        public string Ulice
        {
            get { return _ulice; }
            set
            {
                if (_ulice != value)
                {
                    _ulice = value;
                    OnPropertyChanged(Ulice);
                }
            }
        }
        public string Mesto
        {
            get { return _mesto; }
            set
            {
                if (_mesto != value)
                {
                    _mesto = value;
                    OnPropertyChanged(Mesto);
                }
            }
        }
        public string CisloPopisne
        {
            get { return _cisloPopisne; }
            set
            {
                if (_cisloPopisne != value)
                {
                    _cisloPopisne = value;
                    OnPropertyChanged(CisloPopisne);
                }
            }
        }

        public string PSC
        {
            get { return _psc; }
            set
            {
                if (_psc != value)
                {
                    _psc = value;
                    OnPropertyChanged(PSC);
                }
            }
        }
        public string Stat
        {
            get { return _stat; }
            set
            {
                if (_stat != value)
                {
                    _stat = value;
                    OnPropertyChanged(Stat);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}