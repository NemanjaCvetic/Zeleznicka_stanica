using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models
{
    public class DodatnaStanicaBO
    {
        public int DodatnaStanicaID { get; set; }

        public int RedVoznjeID { get; set; }

        public int StanicaID { get; set; }

        public TimeSpan VremeDolaska { get; set; }

        public TimeSpan VremePolaska { get; set; }
    }
}