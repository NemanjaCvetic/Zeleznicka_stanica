using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models
{
    public class RezervacijaBO
    {
        public int RezervacijaID { get; set; }

        public int KorisnikID { get; set; }

        public int RedVoznjeID { get; set; }

        public DateTime Datum { get; set; }

        public TimeSpan Vreme { get; set; }
    }
}