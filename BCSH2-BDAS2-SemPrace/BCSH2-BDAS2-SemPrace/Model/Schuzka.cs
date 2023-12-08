using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Schuzka : INotifyPropertyChanged
    {
        public int IdSchuzka
        {
            get { return _idSchuzka; }
            set { _idSchuzka = value; OnPropertyChanged(nameof(IdSchuzka)); }
        }
        public DateTime Datum
        {
            get { return _datum; }
            set { _datum = value; OnPropertyChanged(nameof(Datum)); }
        }
        public int IdPobocka
        {
            get { return _idPobocka; }
            set { _idPobocka = value; OnPropertyChanged(nameof(IdPobocka)); }
        }
        public int IdStatus
        {
            get { return _idStatus;
                
                }
            set { _idStatus = value; OnPropertyChanged(nameof(IdStatus)); }
        }
        
        

        private int _idSchuzka;
        private DateTime _datum;
        private int _idPobocka;
        private int _idStatus;



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}