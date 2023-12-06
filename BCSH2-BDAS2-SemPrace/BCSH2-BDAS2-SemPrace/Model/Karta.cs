using System.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Data;

public class Karta : INotifyPropertyChanged
{
    private int _idKarta;
    private string _jmeno;
    private string _prijmeni;
    private long _cisloKarty;
    private string _platebniSystem;
    private DateTime _platnost;
    private string _typ;
    private int _ucetIdUcet;

    public int IdKarta
    {
        get { return _idKarta; }
        set { _idKarta = value; OnPropertyChanged(nameof(IdKarta)); }
    }

    public string Jmeno
    {
        get { return _jmeno; }
        set { _jmeno = value; OnPropertyChanged(nameof(Jmeno)); }
    }

    public string Prijmeni
    {
        get { return _prijmeni; }
        set { _prijmeni = value; OnPropertyChanged(nameof(Prijmeni)); }
    }

    public long CisloKarty
    {
        get { return _cisloKarty; }
        set { _cisloKarty = value; OnPropertyChanged(nameof(CisloKarty)); }
    }

    public string PlatebniSystem
    {
        get { return _platebniSystem; }
        set { _platebniSystem = value; OnPropertyChanged(nameof(PlatebniSystem)); }
    }

    public DateTime Platnost
    {
        get { return _platnost; }
        set { _platnost = value; OnPropertyChanged(nameof(Platnost)); }
    }

    public string Typ
    {
        get { return _typ; }
        set { _typ = value; OnPropertyChanged(nameof(Typ)); }
    }

    public int UcetIdUcet
    {
        get { return _ucetIdUcet; }
        set { _ucetIdUcet = value; OnPropertyChanged(nameof(UcetIdUcet)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static ObservableCollection<Karta> ConvertDataTableToObservableCollection(DataTable dataTable)
    {
        ObservableCollection<Karta> kartaList = new ObservableCollection<Karta>();

        foreach (DataRow row in dataTable.Rows)
        {
            Karta karta = new Karta
            {
                IdKarta = Convert.ToInt32(row["id_karta"]),
                Jmeno = row["jmeno"].ToString(),
                Prijmeni = row["prijmeni"].ToString(),
                CisloKarty = Convert.ToInt32(row["cislo_karty"]),
                PlatebniSystem = row["platebni_system"].ToString(),
                Platnost = Convert.ToDateTime(row["platnost"]),
                Typ = row["typ"].ToString(),
                UcetIdUcet = Convert.ToInt32(row["ucet_id_ucet"])
                // Set other properties based on your Karta class
            };

            kartaList.Add(karta);
        }

        return kartaList;
    }

}