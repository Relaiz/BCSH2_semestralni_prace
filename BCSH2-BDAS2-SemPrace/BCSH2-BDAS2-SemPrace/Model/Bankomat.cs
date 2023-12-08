using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Bankomat
    {
        
        
        
      
        
        private string _adresa;
        private string _popisStatus;
        private string _nazev;
        private int _idStatus;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _idAdresa;
        private int _idBamkomat;
        private int _idBank;
        public int IdBank
        {
            get { return _idBank; }
            set
            {
                if (_idBank != value)
                {
                    _idBank = value;
                    OnPropertyChanged(nameof(IdBank));
                }
            }
        }
        public int IdAdresa
        {
            get { return _idAdresa; }
            set
            {
                if (_idAdresa != value)
                {
                    _idAdresa = value;
                    OnPropertyChanged(nameof(IdAdresa));
                }
            }
        }
        public int IdBamkomat
        {
            get { return _idBamkomat; }
            set
            {
                if (_idBamkomat != value)
                {
                    _idBamkomat = value;
                    OnPropertyChanged(nameof(IdBamkomat));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int IdStatus
        {
            get { return _idStatus; }
            set
            {
                if (_idStatus != value)
                {
                    _idStatus = value;
                    OnPropertyChanged(nameof(IdStatus));
                }
            }
        }
        public string Nazev
        {
            get { return _nazev; }
            set
            {
                if (_nazev != value)
                {
                    _nazev = value;
                    OnPropertyChanged(nameof(Nazev));
                }
            }
        }
        public string StatusPopis
        {
            get { return _popisStatus; }
            set
            {
                if (_popisStatus != value)
                {
                    _popisStatus = value;
                    OnPropertyChanged(nameof(StatusPopis));
                }
            }
        }
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
    }
}