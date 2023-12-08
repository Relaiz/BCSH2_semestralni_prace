using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class KlientSchuzka : INotifyPropertyChanged
    {
       
        public int Schuzka_id_Schuzka
        {
            get { return _schuzka_id_schuzka; }
            set { _schuzka_id_schuzka = value; OnPropertyChanged(nameof(Schuzka_id_Schuzka)); }
        }
       
        
        public int Klient_id_Klient
        {
            get
            {
                return _klient_id_klient;

            }
            set { _klient_id_klient = value; OnPropertyChanged(nameof(Klient_id_Klient)); }
        }



        private int _klient_id_klient;

        private int _schuzka_id_schuzka;



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}