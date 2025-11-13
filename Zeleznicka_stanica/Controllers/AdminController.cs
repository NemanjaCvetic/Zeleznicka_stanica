using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeleznicka_stanica.Models;
using Zeleznicka_stanica.Models.EFRepository;

namespace Zeleznicka_stanica.Controllers
{
    [Authorize(Roles ="admin,radnik")]
    public class AdminController : Controller
    {
        // GET: Admin

        private KorisnikRepository kr = new KorisnikRepository();
        private ZeleznickaStanicaEntities stanica = new ZeleznickaStanicaEntities();


        public ActionResult Index()
        {
            ViewBag.korisnici = kr.GetKorisnici();
            ViewBag.redVoznje = kr.GetAllRedVoznje();
            ViewBag.stanice = kr.GetAllStanice();
            ViewBag.rezervacije = kr.GetAllRezervacije();
            return View();
        }

        public ActionResult IzmeniKorisnika(int ID)
        {
            
            return View(kr.GetKorisnikById(ID));
        }

        [HttpPost]

        public ActionResult IzmeniKorisnika(KorisnikBO korisnik)
        {
            kr.UpdateKorisnik(korisnik);
            return RedirectToAction("Index");
        }

        public ActionResult BrisiKorisnika(int ID)
        {
           KorisnikBO korisnik = kr.GetKorisnikById(ID);
            kr.DeleteKorisnik(korisnik);
            return RedirectToAction("Index");
        }

        public ActionResult IzmeniRedVoznje(int ID)
        {
            return View(kr.GetRedVoznjeById(ID));
        }

        [HttpPost]

        public ActionResult IzmeniRedVoznje(RedVoznjeBO redVoznjeBO)
        {
            
            kr.UpdateRedVoznje(redVoznjeBO);
            return RedirectToAction("Index");
        }

        public ActionResult NapraviRezervacijuAdmin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult NapraviRezervacijuAdmin(RezervacijaBO rezervacija)
        {
            int vozID = stanica.RedVoznje.Where(t => t.RedVoznjeID == rezervacija.RedVoznjeID).Select(t => t.VozID).FirstOrDefault();

            if (kr.DaLiJeVozPun(vozID))
            {
                TempData["Pun"] = "Ovaj voz nema slobodnih mesta";
                return RedirectToAction("Index");
            }

            kr.NapraviRezervacijuAdmin(rezervacija);

            return RedirectToAction("Index");
        }

        public ActionResult IzmeniVremeDodatnaStanica(int DodatnaStanicaID)
        {
            return View(kr.getDodatnaStanicaByID(DodatnaStanicaID));
        }

        [HttpPost]

        public ActionResult IzmeniVremeDodatnaStanica(DodatnaStanicaBO dodatnaStanicaBO)
        {
            kr.UpdateVremeDodatnaStanica(dodatnaStanicaBO);
            return RedirectToAction("Index");
        }

        


    }
}