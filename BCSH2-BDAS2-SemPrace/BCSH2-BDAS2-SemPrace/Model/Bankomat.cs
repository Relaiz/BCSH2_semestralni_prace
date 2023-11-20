using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Bankomat
    {
        public int IdBankomat { get; set; }
        public string Nazev { get; set; }
        public int IdAdres { get; set; }
        public int IdBank { get; set; }
        public int IdStatus { get; set; }
    }
}