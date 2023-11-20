using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Ucet
    {
        public int IdUcet { get; set; }
        public decimal CisloUctu { get; set; }
        public string Nazev { get; set; }
        public int IdKlient { get; set; }
        public int IdBank { get; set; }
        public int IdStatus { get; set; }
    }
}