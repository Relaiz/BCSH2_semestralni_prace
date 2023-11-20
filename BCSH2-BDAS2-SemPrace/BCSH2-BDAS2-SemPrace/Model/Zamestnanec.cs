using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Zamestnanec
    {
        public int IdZamestnanec { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public decimal Mzda { get; set; }
        public string Funkce { get; set; }
        public string TelefoniCislo { get; set; }
        public int IdAdres { get; set; }
        public int IdBank { get; set; }
        public int IdStatus { get; set; }
        public int IdPobocka { get; set; }
    }
}