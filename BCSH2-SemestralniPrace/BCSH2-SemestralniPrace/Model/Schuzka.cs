using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_SemestralniPrace.Model;

public class Schuzka
{
    public int IdSchuzka { get; set; }
    public DateTime Datum { get; set; }
    public int IdPobocka { get; set; }
    public int IdStatus { get; set; }
}

