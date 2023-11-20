using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_BDAS2_SemPrace.Model
{
    public class KreditniKarta : Karta
    {
        public decimal UverzovyLimit { get; set; }
        public decimal BezurocneObdobi { get; set; }
        public decimal UrokovaSazba { get; set; }
        public decimal PovinaPlatbaUveru { get; set; }
    }
}