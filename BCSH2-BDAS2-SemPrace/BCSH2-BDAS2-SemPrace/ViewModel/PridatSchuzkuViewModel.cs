using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class PridatSchuzkuViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TimeSpan> AvailableTimes { get; set; }
        public TimeSpan SelectedTime { get; set; }
        public DateTime? SelectedDate { get; set; }
        public ObservableCollection<Pobocka> Pobocky { get; set; }
        public Pobocka SelectedPobocka { get; set; }
        public ObservableCollection<Zamestnanec> Bankers { get; set; }
        public Zamestnanec SelectedBanker { get; set; }
        public ICommand AddSchuzkaCommand { get; private set; }
        public PridatSchuzkuViewModel(Klient klient)
        {

            AvailableTimes = new ObservableCollection<TimeSpan>();
            for (int hour = 8; hour < 18; hour++)
            {
                AvailableTimes.Add(new TimeSpan(hour, 0, 0));
                AvailableTimes.Add(new TimeSpan(hour, 30, 0));
            }
            SelectedTime = AvailableTimes.First();
            SelectedDate = DateTime.Today;
            Pobocky = new ObservableCollection<Pobocka>(); 
            Bankers = new ObservableCollection<Zamestnanec>(); 
            AddSchuzkaCommand = new RelayCommand(AddSchuzka, CanAddSchuzka);

        }
        private void AddSchuzka(object parameter)
        {
            // Логика добавления встречи
            DateTime scheduledDateTime = SelectedDate.Value.Add(SelectedTime);

            // Создание объекта Schuzka и заполнение его свойств
            Schuzka newSchuzka = new Schuzka
            {
                Datum = scheduledDateTime,
                IdPobocka = SelectedPobocka.IdPobocka,
               // IdStatus = /* Здесь нужен ID начального статуса встречи */,
                // Другие свойства по необходимости
            };

            // Сохранение новой встречи в базе данных
            // ...

            // Обновление UI или сообщение пользователю
            // ...
        }
        private bool CanAddSchuzka(object parameter)
        {
            // Проверка возможности добавления встречи
            return SelectedDate.HasValue && SelectedPobocka != null && SelectedBanker != null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
