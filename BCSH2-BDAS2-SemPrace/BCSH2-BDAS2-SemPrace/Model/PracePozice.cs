using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class PracePozice : INotifyPropertyChanged
    {
        private int id_pozice;
        private string pozice_nazev;
        private int min_mzda  ;
        private int max_mzda ;
        public PracePozice() {
        
        }
        public int IdPozice
        {
            get { return id_pozice; }
            set
            {
                if (id_pozice != value)
                {
                    id_pozice = value;
                    OnPropertyChanged(nameof(IdPozice));
                }
            }
        }
        public string Pozice 
        {
            get { return pozice_nazev; }
            set
            {
                if (pozice_nazev != value)
                {
                    pozice_nazev = value;
                    OnPropertyChanged(nameof(Pozice));
                }
            }
        }
        public int MinMzda
        {
            get { return min_mzda; }
            set
            {
                if (min_mzda != value)
                {
                    min_mzda = value;
                    OnPropertyChanged(nameof(MinMzda));
                }
            }
        }
        public int MaxMzda
        {
            get { return max_mzda; }
            set
            {
                if (max_mzda != value)
                {
                    max_mzda = value;
                    OnPropertyChanged(nameof(MaxMzda));
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
