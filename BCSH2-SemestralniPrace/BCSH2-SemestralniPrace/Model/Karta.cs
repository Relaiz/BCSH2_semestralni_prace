using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_SemestralniPrace.Model;

public abstract class Karta
{
    public int IdKarta { get; set; }
    public decimal CisloKarty { get; set; }
    public string PlatebniSystem { get; set; }
    public DateTime Platnost { get; set; }
    public string Typ { get; set; }
    public int IdUcet { get; set; }
    public string Jmeno { get; set; }
    public string Prijmeni { get; set; }
    // Add common properties and methods here
}

