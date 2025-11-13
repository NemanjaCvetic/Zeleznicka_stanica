using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeleznicka_stanica.Models;
using Zeleznicka_stanica.Models.EFRepository;

namespace Zeleznicka_stanica.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Proba
        private Models.EFRepository.KorisnikRepository kr = new KorisnikRepository();
        private ZeleznickaStanicaEntities stanica = new ZeleznickaStanicaEntities();
        public ActionResult Pocetna()
        {


            //var redVoznje = from rv in stanica.RedVoznje
            //                join s in stanica.Stanica on rv.PolazakID equals s.StanicaID
            //                select new  { s.StanicaID, s.Grad };

            var redVoznje = from s in stanica.Stanica
                            select new { s.StanicaID, s.Grad };

            ViewBag.Polazak = new SelectList(redVoznje, "StanicaID", "Grad");

            return View("Index");
        }

        [HttpPost]

        public ActionResult RedVoznjeForm(int? id, int? Odrediste, DateTime datum)
        {
            //var redVoznje = (from rv in stanica.RedVoznje
            //                 join ds in stanica.DodatnaStanica on rv.RedVoznjeID equals ds.RedVoznjeID
            //                 where rv.PolazakID == id && ds.StanicaID == Odrediste
            //                 select new RedVoznjeView
            //                 {
            //                     RedVoznjeID = rv.RedVoznjeID,
            //                     VozID = rv.VozID,
            //                     Datum = rv.Datum,
            //                     Vreme = rv.Vreme,
            //                     Peron = rv.Peron,
            //                     Kasni = rv.Kasni,
            //                     PolazakID = rv.PolazakID,
            //                     OdredisteID = rv.OdredisteID,
            //                     Cena = rv.Cena,
            //                     Otkazan = rv.Otkazan,
            //                     DodatnaStanicaID = ds.DodatnaStanicaID,
            //                     StanicaID = ds.StanicaID,
            //                     VremeDolaska = ds.VremeDolaska,
            //                     VremePolaska = ds.VremePolaska 
            //                 }).ToList();


            //ViewBag.RedVoznje = stanica.RedVoznje.Where(t => t.PolazakID == id ).ToList();
            if(id == null || Odrediste == null)
            {
                TempData["Unos"] = "Morate uneti sve podatke";
                return RedirectToAction("Pocetna");
            }
            ViewBag.Datum = datum;
            ViewBag.RedVoznje = kr.RedVoznjeDodatnaStanica(id, Odrediste,datum);
            ViewBag.stanice = kr.GetAllStanice();


            return View("form1");
        }

        public ActionResult PrikazRezervacije()
        {
            ViewBag.redVoznje = kr.GetAllRedVoznje();
            ViewBag.Rezervacije = kr.PrikazRezervacije();
            return View();
        }
        [Authorize]
        [HttpGet]

        public ActionResult NapraviRezervaciju(int RedVoznjeID, int VozID)
        {
            if(kr.DaLiJeVozPun(VozID))
            {
                TempData["Pun"] = "Ovaj voz nema slobodnih mesta";
                return RedirectToAction("Pocetna");
            }

            
           kr.NapraviRezervaciju(RedVoznjeID, VozID);

            return RedirectToAction("Pocetna");
        }

        public ActionResult BrisiRezervaciju(int RezervacijaID)
        {

            kr.BrisiRezervaciju(RezervacijaID);
            return RedirectToAction("Index","Home");
        }

        public ActionResult RedVoznjeDetalji(int RedVoznjeID, int VozID)
        {

            

            ViewBag.Stanice = kr.GetAllStanice();
            ViewBag.Cena = stanica.RedVoznje.Where(t => t.RedVoznjeID == RedVoznjeID).Select(t => t.Cena).FirstOrDefault();
            ViewBag.SlobodnaMesta = stanica.Voz.Where(t => t.VozID == VozID).Select(t => t.BrojSlobodnihMesta).FirstOrDefault();
            
            return View(kr.GetDodatneStanice(RedVoznjeID));
        }



    }

}