﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BCSH2_BDAS2_SemPrace.Model
{
    public class Klient
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public int AdresaId { get; set; }
        public int BankId { get; set; }
        public string TelefonniCislo { get; set; }
        public int ZamestnanecId { get; set; }
    }
}