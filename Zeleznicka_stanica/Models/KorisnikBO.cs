using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models
{
    public class KorisnikBO
    {
        public int KorisnikID { get; set; }

        public int? StanicaID { get; set; }
        [Required(ErrorMessage ="Morate uneti ime")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Morate uneti prezime")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Morate uneti email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Morate uneti sifru")]
        public string Sifra { get; set; }

        public string Status { get; set; }



    }
}