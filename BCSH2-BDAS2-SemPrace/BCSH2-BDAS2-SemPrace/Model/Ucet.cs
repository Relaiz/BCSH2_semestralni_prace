using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Data;

public class Ucet : INotifyPropertyChanged
{
    private int _idUcet;
    private int _cisloUctu;
    private string _nazev;
    private int _klientIdKlient;
    private int _bankIdBank;
    private int _statusIdStatus;

    public int IdUcet
    {
        get { return _idUcet; }
        set { _idUcet = value; OnPropertyChanged(nameof(IdUcet)); }
    }

    public int CisloUctu
    {
        get { return _cisloUctu; }
        set { _cisloUctu = value; OnPropertyChanged(nameof(CisloUctu)); }
    }

    public string Nazev
    {
        get { return _nazev; }
        set { _nazev = value; OnPropertyChanged(nameof(Nazev)); }
    }

    public int KlientIdKlient
    {
        get { return _klientIdKlient; }
        set { _klientIdKlient = value; OnPropertyChanged(nameof(KlientIdKlient)); }
    }

    public int BankIdBank
    {
        get { return _bankIdBank; }
        set { _bankIdBank = value; OnPropertyChanged(nameof(BankIdBank)); }
    }

    public int StatusIdStatus
    {
        get { return _statusIdStatus; }
        set { _statusIdStatus = value; OnPropertyChanged(nameof(StatusIdStatus)); }
    }

    // Add other properties as needed

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static ObservableCollection<Ucet> ConvertDataTableToObservableCollection(DataTable dataTable)
    {
        ObservableCollection<Ucet> ucetList = new ObservableCollection<Ucet>();

        foreach (DataRow row in dataTable.Rows)
        {
            Ucet ucet = new Ucet
            {
                IdUcet = Convert.ToInt32(row["id_ucet"]),
                CisloUctu = Convert.ToInt32(row["cislo_uctu"]),
                Nazev = row["nazev"].ToString(),
                KlientIdKlient = Convert.ToInt32(row["klient_id_klient"]),
                BankIdBank = Convert.ToInt32(row["bank_id_bank"]),
                StatusIdStatus = Convert.ToInt32(row["status_id_status"])
                // Set other properties based on your Ucet class
            };

            ucetList.Add(ucet);
        }

        return ucetList;
    }
}