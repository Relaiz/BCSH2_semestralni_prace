using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

public class Operace : INotifyPropertyChanged
{
    private int _idOperace;
    private decimal _castka;
    private DateTime _datumZacatka;
    private DateTime _datumOkonceni;
    private string _nazev;
    private long _zUctu;
    private long _doUctu;
    private int _idUcet;
    private int _idStatus;
    private string _popisStatusu;

    public int IdOperace
    {
        get { return _idOperace; }
        set { _idOperace = value; OnPropertyChanged(nameof(IdOperace)); }
    }

    public decimal Castka
    {
        get { return _castka; }
        set { _castka = value; OnPropertyChanged(nameof(Castka)); }
    }

    public DateTime DatumZacatka
    {
        get { return _datumZacatka; }
        set { _datumZacatka = value; OnPropertyChanged(nameof(DatumZacatka)); }
    }

    public DateTime DatumOkonceni
    {
        get { return _datumOkonceni; }
        set { _datumOkonceni = value; OnPropertyChanged(nameof(DatumOkonceni)); }
    }

    public string Nazev
    {
        get { return _nazev; }
        set { _nazev = value; OnPropertyChanged(nameof(Nazev)); }
    }

    public long ZUctu
    {
        get { return _zUctu; }
        set { _zUctu = value; OnPropertyChanged(nameof(ZUctu)); }
    }

    public long DoUctu
    {
        get { return _doUctu; }
        set { _doUctu = value; OnPropertyChanged(nameof(DoUctu)); }
    }

    public int IdUcet
    {
        get { return _idUcet; }
        set { _idUcet = value; OnPropertyChanged(nameof(IdUcet)); }
    }

    public int IdStatus
    {
        get { return _idStatus; }
        set { _idStatus = value; OnPropertyChanged(nameof(IdStatus)); }
    }

    public string PopisStatusu
    {
        get { return _popisStatusu; }
        set { _popisStatusu = value; OnPropertyChanged(nameof(PopisStatusu)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public static ObservableCollection<Operace> ConvertDataTableToObservableCollection(DataTable dataTable)
    {
        ObservableCollection<Operace> operaceList = new ObservableCollection<Operace>();

        foreach (DataRow row in dataTable.Rows)
        {
            Operace operace = new Operace
            {
                IdOperace = Convert.ToInt32(row["id_operace"]),
                Castka = Convert.ToDecimal(row["castka"]),
                DatumZacatka = Convert.ToDateTime(row["datum_zacatka"]),
                DatumOkonceni = Convert.ToDateTime(row["datum_okonceni"]),
                Nazev = row["nazev"].ToString(),
                ZUctu = Convert.ToInt32(row["z_uctu"]),
                DoUctu = Convert.ToInt32(row["do_uctu"]),
                IdUcet = Convert.ToInt32(row["ucet_id_ucet"]),
                IdStatus = Convert.ToInt32(row["status_id_status"])
                // Set other properties based on your Operace class
            };

            operaceList.Add(operace);
        }

        return operaceList;
    }
}
