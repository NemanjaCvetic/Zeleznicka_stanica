using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models
{
    public class RedVoznjeBO
    {
        public int RedVoznjeID { get; set; }


        public int VozID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        public TimeSpan Vreme { get; set; }

        public int Peron { get; set; }

        public string Kasni { get; set; }

        public int PolazakID { get; set; }

        public int OdredisteID { get; set; }

        public int? Cena { get; set; }

        public string Otkazan { get; set; }
    }
}