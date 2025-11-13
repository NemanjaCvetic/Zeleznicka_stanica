using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeleznicka_stanica.Models.EFRepository;

namespace Zeleznicka_stanica.Controllers
{
    public class HomeController : Controller
    {
        private KorisnikRepository kr = new KorisnikRepository();
        private ZeleznickaStanicaEntities stanica = new ZeleznickaStanicaEntities();
        public ActionResult Index()
        {


            //var redVoznje = from rv in stanica.RedVoznje
            //                join s in stanica.Stanica on rv.PolazakID equals s.StanicaID
            //                select new  { s.StanicaID, s.Grad };

            var redVoznje = from s in stanica.Stanica                           
                            select new { s.StanicaID, s.Grad };

            ViewBag.Polazak = new SelectList(redVoznje, "StanicaID", "Grad");

            return View();
        }

       

        public PartialViewResult GetOdrediste(int redVoznjeID) // polazakID
        {
            //List<DodatnaStanica> redVoznje = new List<DodatnaStanica>();
            //redVoznje = stanica.DodatnaStanica.Where(t => t.RedVoznjeID == redVoznjeID).ToList();

            //var redVoznje = from rv in stanica.RedVoznje
            //                where rv.PolazakID == redVoznjeID
            //                select rv.RedVoznjeID;


            //var stanice = (from ds in stanica.DodatnaStanica
            //              join s in stanica.Stanica on ds.StanicaID equals s.StanicaID  
            //              join rv in stanica.RedVoznje on ds.RedVoznjeID equals rv.RedVoznjeID
            //              where redVoznje.Contains(ds.RedVoznjeID) && rv.PolazakID == redVoznjeID
            //              select new { ds.StanicaID, s.Grad }).Distinct(); // da se ne bi ponavljale iste vrednosti u drugoj dropdown listi
                          

            

            SelectList dodatnaStanica = new SelectList(kr.GetOdrediste(redVoznjeID), "StanicaID", "Grad");
            ViewBag.Polazak = dodatnaStanica;
            return PartialView("OdredistePartial", dodatnaStanica);


        }

        [HttpPost]

        public ActionResult form1(int id)
        {

            ViewBag.RedVoznje = stanica.RedVoznje.Where(t => t.PolazakID == id).ToList();


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       

    }
}