// Zustatek model class
using BCSH2_BDAS2_SemPrace.ViewModel;
using System;

public class Zustatek
{
    public int IdZustatek { get; set; }
    public decimal BlokovaneCastka { get; set; }
    public decimal VolnaCastka { get; set; }
    public DateTime Datum { get; set; }
    public int IdUcet { get; set; }
}

// ZustatekViewModel class
public class ZustatekViewModel : ViewModelBase
{
    private int _idZustatek;
    public int IdZustatek
    {
        get { return _idZustatek; }
        set
        {
            _idZustatek = value;
            OnPropertyChanged(nameof(IdZustatek));
        }
    }

    private decimal _blokovaneCastka;
    public decimal BlokovaneCastka
    {
        get { return _blokovaneCastka; }
        set
        {
            _blokovaneCastka = value;
            OnPropertyChanged(nameof(BlokovaneCastka));
        }
    }

    private decimal _volnaCastka;
    public decimal VolnaCastka
    {
        get { return _volnaCastka; }
        set
        {
            _volnaCastka = value;
            OnPropertyChanged(nameof(VolnaCastka));
        }
    }

    private DateTime _datum;
    public DateTime Datum
    {
        get { return _datum; }
        set
        {
            _datum = value;
            OnPropertyChanged(nameof(Datum));
        }
    }

    private int _idUcet;
    public int IdUcet
    {
        get { return _idUcet; }
        set
        {
            _idUcet = value;
            OnPropertyChanged(nameof(IdUcet));
        }
    }
}
