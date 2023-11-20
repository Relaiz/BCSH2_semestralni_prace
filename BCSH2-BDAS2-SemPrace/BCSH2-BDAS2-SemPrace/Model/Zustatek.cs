using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Zustatek
    {
        public int IdZustatek { get; set; }
        public decimal BlokovaneCastka { get; set; }
        public decimal VolnaCastka { get; set; }
        public DateTime Datum { get; set; }
        public int IdUcet { get; set; }
    }

}