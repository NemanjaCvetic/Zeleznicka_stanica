using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models
{
    public class RedVoznjeView
    {
        public int RedVoznjeID { get; set; }


        public int VozID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        public TimeSpan Vreme { get; set; }

        public int Peron { get; set; }

        public bool Kasni { get; set; }

        public int PolazakID { get; set; }

        public int OdredisteID { get; set; }

        public int? Cena { get; set; }

        public bool? Otkazan { get; set; }

        public int? DodatnaStanicaID { get; set; }

        public int? RedVoznjeIDDS { get; set; }

        public int? StanicaID { get; set; }

        public TimeSpan? VremeDolaska { get; set; }

        public TimeSpan? VremePolaska { get; set; }


    }
}