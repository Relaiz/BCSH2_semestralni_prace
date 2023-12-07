using BCSH2_BDAS2_SemPrace.Commands;
using BCSH2_BDAS2_SemPrace.DataBase;
using BCSH2_BDAS2_SemPrace.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BCSH2_BDAS2_SemPrace.ViewModel
{
    public class KlientObjednaniKartyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> platebniSystems;
        public ObservableCollection<string> PlatebniSystems
        {
            get { return platebniSystems; }
            set
            {
                platebniSystems = value;
                OnPropertyChanged(nameof(PlatebniSystems));
            }
        }

        private ObservableCollection<string> cardTypes;
        public ObservableCollection<string> CardTypes
        {
            get { return cardTypes; }
            set
            {
                cardTypes = value;
                OnPropertyChanged(nameof(CardTypes));
            }
        }

        private string selectedPlatebniSystem;
        public string SelectedPlatebniSystem
        {
            get { return selectedPlatebniSystem; }
            set
            {
                selectedPlatebniSystem = value;
                OnPropertyChanged(nameof(SelectedPlatebniSystem));
            }
        }

        private string selectedCardType;
        public string SelectedCardType
        {
            get { return selectedCardType; }
            set
            {
                selectedCardType = value;
                OnPropertyChanged(nameof(SelectedCardType));
            }
        }

        private string klientName;
        public string KlientName
        {
            get { return klientName; }
            set
            {
                klientName = value;
                OnPropertyChanged(nameof(KlientName));
            }
        }

        private string klientSurname;
        public string KlientSurname
        {
            get { return klientSurname; }
            set
            {
                klientSurname = value;
                OnPropertyChanged(nameof(KlientSurname));
            }
        }

        public ICommand ObjednatKartuCommand { get; }
        public ICommand ZavritOknoCommand { get; }
        private Ucet _currentUcet;
        private Klient _currentKlient;
        private readonly OracleDatabaseService db;


        public KlientObjednaniKartyViewModel(Ucet ucet, Klient klient)
        {
            PlatebniSystems = new ObservableCollection<string> { "American Express", "MasterCard", "Maestro", "Visa" };
            CardTypes = new ObservableCollection<string> { "D", "K" };
            _currentUcet = ucet;
            _currentKlient = klient;
            db = new OracleDatabaseService();
            db.OpenConnection();

            ObjednatKartuCommand = new RelayCommand(ObjednatKartu);
            ZavritOknoCommand = new RelayCommand(ZavritOkno);
        }

        private void ObjednatKartu(object parameter)
        {
            // Check if Platebni System and Card Type are selected
            if (!string.IsNullOrEmpty(SelectedPlatebniSystem) && !string.IsNullOrEmpty(SelectedCardType))
            {

                // Implement logic for ordering the card using the provided parameters
                try
                {
                    // Call the PL/SQL procedure to order a new card
                    db.OrderNewCard(
                        _currentUcet.IdUcet,
                        _currentKlient.Jmeno,
                        _currentKlient.Prijmeni,
                        1,
                        SelectedPlatebniSystem,
                        DateTime.Now.AddYears(4),  // Assuming you want to set the Platnost for 4 years from now
                        SelectedCardType
                    );

                    // Show a success message
                    MessageBoxResult result = MessageBox.Show("Karta objednana!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        // Close the window
                        Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
                    }



                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log, show error message)
                    MessageBox.Show($"Chyba objednani nove karty: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Show a message to the user that Platebni System and Card Type must be selected
                MessageBox.Show("Prosim, vyberte platebni system a typ karty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ZavritOkno(object parameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CloseRequested;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }

}
