using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_SemestralniPrace.Model;

public class Pobocka
{
    public int IdPobocka { get; set; }
    public string Nazev { get; set; }
    public string TelefoniCislo { get; set; }
    public int IdAdres { get; set; }
    public int IdBank { get; set; }
    public int IdStatus { get; set; }
}

