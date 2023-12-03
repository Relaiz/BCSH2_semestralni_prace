using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class User : INotifyPropertyChanged
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }
        // Дополнительные общие свойства
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum UserType
    {
        Klient,
        Zamestnanec,
        Admin,
        Banker,
        Manazer
    }
}
