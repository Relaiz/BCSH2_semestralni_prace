using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class ZamestnanecViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ZamestnanecModel> _zamestnanci;
        public ObservableCollection<ZamestnanecModel> Zamestnanci
        {
            get { return _zamestnanci; }
            set
            {
                _zamestnanci = value;
                OnPropertyChanged(nameof(Zamestnanci));
            }
        }

        public ICommand LoadZamestnanciCommand { get; }

        private readonly OracleDatabaseService _databaseService;

        public ZamestnanecViewModel()
        {
            LoadZamestnanciCommand = new RelayCommand(LoadZamestnanci);
            _databaseService = new OracleDatabaseService();
        }

        private void LoadZamestnanci(object parameter)
        {
            // Используйте _databaseService для выполнения запроса к базе данных Oracle и обновите Zamestnanci
            string query = "SELECT * FROM zamestnanec";
            DataTable result = _databaseService.ExecuteQuery(query);

            // Преобразуйте результат в ObservableCollection<ZamestnanecModel> и обновите Zamestnanci
            // ...
        }

        // Добавьте другие методы и обработчики команд по необходимости

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
