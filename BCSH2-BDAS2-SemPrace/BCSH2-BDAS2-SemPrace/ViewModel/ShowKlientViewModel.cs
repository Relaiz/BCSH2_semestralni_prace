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
        private Klient _currentKlient;
        private readonly OracleDatabaseService db;
        public Klient CurrentKlient
        {
            get { return _currentKlient; }

            set
            {
                _currentKlient = value;
                OnPropertyChanged(nameof(CurrentKlient));
            }
        }

        public ShowKlientViewModel(Klient klient)
        {
            // LoadZamestnanciCommand = new RelayCommand(LoadZamestnanci);

            db = new OracleDatabaseService();
            db.OpenConnection();

            CurrentKlient = klient;
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
