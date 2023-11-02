using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_SemestralniPrace.Model;

public class Operace
{
    public int IdOperace { get; set; }
    public decimal Castka { get; set; }
    public DateTime DatumZacatka { get; set; }
    public DateTime DatumOkonceni { get; set; }
    public string Nazev { get; set; }
    public int ZUctu { get; set; }
    public int DoUctu { get; set; }
    public int IdUcet { get; set; }
    public int IdStatus { get; set; }
}

