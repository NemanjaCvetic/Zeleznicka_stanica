using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeleznicka_stanica.Models.EFRepository
{
    public class KorisnikRepository
    {
        private ZeleznickaStanicaEntities stanica = new ZeleznickaStanicaEntities();

        public KorisnikBO GetKorisnikById(int id)
        {
            Korisnik korisnik = stanica.Korisnik.Where(t => t.KorisnikID == id).FirstOrDefault();

            KorisnikBO korisnikBO = new KorisnikBO()
            {
                KorisnikID = korisnik.KorisnikID,
                StanicaID = korisnik.StanicaID,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Email = korisnik.Email,
                Sifra = korisnik.Sifra,
                Status = korisnik.Status

            };

            return korisnikBO;
        }

        public IEnumerable<KorisnikBO> GetKorisnici()
        {
            List<KorisnikBO> korisnici = new List<KorisnikBO>();

            foreach (Korisnik k in stanica.Korisnik)
            {
                KorisnikBO korisnikBo = new KorisnikBO()
                {
                    KorisnikID = k.KorisnikID,
                    StanicaID = k.StanicaID,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    Email = k.Email,
                    Sifra = k.Sifra,
                    Status = k.Status
                };

                korisnici.Add(korisnikBo);
            }

            return korisnici;
        }

        public void UpdateKorisnik(KorisnikBO korisnikBO)
        {
            Korisnik korisnik = stanica.Korisnik.Where(t => t.KorisnikID == korisnikBO.KorisnikID).FirstOrDefault();

            if (korisnikBO == null) return;
            // if (stanica.Korisnik.Any(t => t.Email == korisnikBO.Email)) return;  
            if (korisnikBO.StanicaID != null)
            {
                korisnik.StanicaID = korisnikBO.StanicaID;
            }

            if (korisnikBO.Ime != null)
            {
                korisnik.Ime = korisnikBO.Ime;
            }

            korisnik.Prezime = korisnikBO.Prezime;


            if (korisnikBO.Status == null)
            {
                korisnik.Status = "putnik";
            }
            else
            {
                korisnik.Status = korisnikBO.Status;
            }


            try
            {
                stanica.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void DeleteKorisnik(KorisnikBO korisnikBO)
        {
           Korisnik korisnik = stanica.Korisnik.Where(t => t.KorisnikID == korisnikBO.KorisnikID).FirstOrDefault();

            if (korisnikBO == null) return;

            stanica.Korisnik.Remove(korisnik);

            try
            {
                stanica.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public RedVoznjeBO GetRedVoznjeById(int id)
        {
            RedVoznje redVoznje = stanica.RedVoznje.Where(t => t.RedVoznjeID == id).FirstOrDefault();

            RedVoznjeBO redVoznjeBO = new RedVoznjeBO()
            {
                RedVoznjeID = redVoznje.RedVoznjeID,               
                VozID = redVoznje.VozID,
                Datum = redVoznje.Datum,
                Vreme = redVoznje.Vreme,              
                Peron = redVoznje.Peron,             
                PolazakID = redVoznje.PolazakID,
                OdredisteID = redVoznje.OdredisteID,
                Cena = redVoznje.Cena

            };

            if (redVoznje.Kasni == false)
                redVoznjeBO.Kasni = "NE";
            else
                redVoznjeBO.Kasni = "DA";

            if (redVoznje.Otkazan == false)
                redVoznjeBO.Otkazan = "NE";
            else
                redVoznjeBO.Otkazan = "DA";

            return redVoznjeBO;
        }

        public IEnumerable<RedVoznjeBO> GetAllRedVoznje ()
        {
            List<RedVoznjeBO> redVoznje = new List<RedVoznjeBO>();
            List<RedVoznjeBO> red = new List<RedVoznjeBO>();

            foreach (RedVoznje rv in stanica.RedVoznje)
            {
                RedVoznjeBO redVoznjeBO = new RedVoznjeBO()
                {
                    RedVoznjeID = rv.RedVoznjeID,
                    VozID = rv.VozID,
                    Datum = rv.Datum,
                    Vreme = rv.Vreme,
                    Peron = rv.Peron,                   
                    PolazakID = rv.PolazakID,
                    OdredisteID =rv.OdredisteID,
                    Cena =rv.Cena

                };
                if (rv.Kasni == false)
                    redVoznjeBO.Kasni = "NE";
                else
                    redVoznjeBO.Kasni = "DA";

                if (rv.Otkazan == false)
                    redVoznjeBO.Otkazan = "NE";
                else
                    redVoznjeBO.Otkazan = "DA";

                redVoznje.Add(redVoznjeBO);

                
                    
                 red =  redVoznje.OrderBy(RvDatum => RvDatum.Datum).ToList();


            }

            return red;
        }

        public IEnumerable<RedVoznjeView> RedVoznjeDodatnaStanica(int? id, int? Odrediste, DateTime datum)
        {
            var redVoznje = (from rv in stanica.RedVoznje
                             join ds in stanica.DodatnaStanica on rv.RedVoznjeID equals ds.RedVoznjeID                   
                             where rv.PolazakID == id && ds.StanicaID == Odrediste && (rv.Datum == datum || rv.Datum.Month == datum.Month)
                             orderby rv.Datum
                             select new RedVoznjeView
                             {
                                 RedVoznjeID = rv.RedVoznjeID,
                                 VozID = rv.VozID,
                                 Datum = rv.Datum,
                                 Vreme = rv.Vreme,
                                 Peron = rv.Peron,
                                 Kasni = rv.Kasni,
                                 PolazakID = rv.PolazakID,
                                 OdredisteID = rv.OdredisteID,
                                 Cena = rv.Cena,
                                 Otkazan = rv.Otkazan,
                                 DodatnaStanicaID = ds.DodatnaStanicaID,
                                 StanicaID = ds.StanicaID,
                                 VremeDolaska = ds.VremeDolaska,
                                 VremePolaska = ds.VremePolaska
                             }).ToList();

            return redVoznje;

            
        }

        public IEnumerable<StanicaBO> GetOdrediste (int polazakID)
        {
            var redVoznje = from rv in stanica.RedVoznje
                            where rv.PolazakID == polazakID
                            select rv.RedVoznjeID;


            var stanice = (from ds in stanica.DodatnaStanica
                           join s in stanica.Stanica on ds.StanicaID equals s.StanicaID
                           join rv in stanica.RedVoznje on ds.RedVoznjeID equals rv.RedVoznjeID
                           where redVoznje.Contains(ds.RedVoznjeID) && rv.PolazakID == polazakID
                           select new StanicaBO 
                           { 
                               StanicaID = ds.StanicaID, 
                               Grad = s.Grad }).Distinct(); // da se ne bi ponavljale iste vrednosti u drugoj dropdown listi

            return stanice;
        }

        public IEnumerable<StanicaBO> GetAllStanice ()
        {
            List<StanicaBO> stanice = new List<StanicaBO>();

            foreach(Stanica s in stanica.Stanica)
            {
                StanicaBO stanicaBO = new StanicaBO()
                {
                    StanicaID = s.StanicaID,
                    Grad = s.Grad,
                    Zemlja = s.Zemlja
                };

                stanice.Add(stanicaBO);
            }

            return stanice;
        }

        public void UpdateRedVoznje(RedVoznjeBO redVoznjeBO)
        {
            RedVoznje redVoznje = stanica.RedVoznje.Where(t => t.RedVoznjeID == redVoznjeBO.RedVoznjeID).FirstOrDefault();

            if (redVoznjeBO == null) return;

            redVoznje.VozID = redVoznjeBO.VozID;
            redVoznje.Datum = redVoznjeBO.Datum;
            redVoznje.Vreme = redVoznjeBO.Vreme;           
            redVoznje.Peron = redVoznjeBO.Peron;

            if (redVoznjeBO.Kasni == "NE")           
                redVoznje.Kasni = false;
            
            else
                redVoznje.Kasni = true;

            redVoznje.PolazakID = redVoznjeBO.PolazakID;
            redVoznje.OdredisteID = redVoznjeBO.OdredisteID;
            redVoznje.Cena = redVoznjeBO.Cena;

            if (redVoznjeBO.Otkazan == "NE")
                redVoznje.Otkazan = false;
            else
                redVoznje.Otkazan = true;

            stanica.SaveChanges();

        }

        public IEnumerable<DodatnaStanicaBO> GetDodatneStanice(int RedVoznjeID)
        {
            List<DodatnaStanicaBO> dodatneStanice = new List<DodatnaStanicaBO>();
            List<DodatnaStanicaBO> dodatneStaniceSortiran = new List<DodatnaStanicaBO>();


            foreach ( DodatnaStanica ds in stanica.DodatnaStanica.Where(t=> t.RedVoznjeID == RedVoznjeID))
            {
                DodatnaStanicaBO dodatnaStanicaBO = new DodatnaStanicaBO()
                {
                    DodatnaStanicaID = ds.DodatnaStanicaID,
                    RedVoznjeID = ds.RedVoznjeID,
                    StanicaID = ds.StanicaID,
                    VremeDolaska = ds.VremeDolaska,
                    VremePolaska = ds.VremePolaska
                };

                dodatneStanice.Add(dodatnaStanicaBO);
            }

            dodatneStaniceSortiran = dodatneStanice.OrderBy(t => t.VremeDolaska).ToList();

            return dodatneStaniceSortiran;
        }

        public DodatnaStanicaBO getDodatnaStanicaByID(int DodatnaStanicaID)
        {
            DodatnaStanica ds = stanica.DodatnaStanica.Where(t => t.DodatnaStanicaID == DodatnaStanicaID).FirstOrDefault();

            DodatnaStanicaBO dodatnaStanicaBO = new DodatnaStanicaBO()
            {
                DodatnaStanicaID = ds.DodatnaStanicaID,
                RedVoznjeID = ds.RedVoznjeID,
                StanicaID = ds.StanicaID,
                VremeDolaska = ds.VremeDolaska,
                VremePolaska = ds.VremePolaska
            };

            return dodatnaStanicaBO;
        }

        public void UpdateVremeDodatnaStanica(DodatnaStanicaBO dodatnaStanicaBO)
        {
            DodatnaStanica ds = stanica.DodatnaStanica.Where(t => t.DodatnaStanicaID == dodatnaStanicaBO.DodatnaStanicaID).FirstOrDefault();

            ds.VremeDolaska = dodatnaStanicaBO.VremeDolaska;
            ds.VremePolaska = dodatnaStanicaBO.VremePolaska;

            stanica.SaveChanges();
        }

        public bool DaLiJeVozPun(int VozID)
        {
            bool pun = false;

            int brojSlobodnihMesta = stanica.Voz.Where(t => t.VozID == VozID).Select(t => t.BrojSlobodnihMesta).FirstOrDefault();

            if (brojSlobodnihMesta < 1)
                pun = true;

            return pun;
        }
        public void NapraviRezervaciju(int RedVoznjeID, int VozID)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;

            int korisnikID = stanica.Korisnik.Where(t => t.Email == email).Select(t => t.KorisnikID).FirstOrDefault();

            Rezervacija r = new Rezervacija()
            {
                KorisnikID = korisnikID,
                RedVoznjeID = RedVoznjeID,
                Datum = System.DateTime.Now.Date,
                Vreme = System.DateTime.Now.TimeOfDay

            };

            Voz voz = stanica.Voz.Where(t => t.VozID == VozID).FirstOrDefault();


            
            voz.BrojSlobodnihMesta = voz.BrojSlobodnihMesta - 1;

            
            stanica.Rezervacija.Add(r);
            stanica.SaveChanges();
        }

        public void NapraviRezervacijuAdmin(RezervacijaBO rezervacija)
        {
            Rezervacija r = new Rezervacija()
            {
                KorisnikID = rezervacija.KorisnikID,
                RedVoznjeID = rezervacija.RedVoznjeID,
                Datum = System.DateTime.Now.Date,
                Vreme = System.DateTime.Now.TimeOfDay

            };

            int vozID = stanica.RedVoznje.Where(t => t.RedVoznjeID == rezervacija.RedVoznjeID).Select(t => t.VozID).FirstOrDefault();

            Voz voz = stanica.Voz.Where(t => t.VozID == vozID).FirstOrDefault();

            voz.BrojSlobodnihMesta = voz.BrojSlobodnihMesta - 1;

            stanica.Rezervacija.Add(r);
            stanica.SaveChanges();

        }

        public void BrisiRezervaciju(int RezervacijaID)
        {
            Rezervacija r = stanica.Rezervacija.Where(t => t.RezervacijaID == RezervacijaID).FirstOrDefault();

            int vozID = stanica.RedVoznje.Where(t => t.RedVoznjeID == r.RedVoznjeID).Select(t => t.VozID).FirstOrDefault();
            Voz voz = stanica.Voz.Where(t => t.VozID == vozID).FirstOrDefault();
            voz.BrojSlobodnihMesta = voz.BrojSlobodnihMesta + 1;

            stanica.Rezervacija.Remove(r);

            stanica.SaveChanges();
        }

        public IEnumerable<RezervacijaBO> GetAllRezervacije()
        {
            List<RezervacijaBO> rezervacije = new List<RezervacijaBO>();

            foreach(Rezervacija r in stanica.Rezervacija)
            {
                RezervacijaBO rezervacijaBO = new RezervacijaBO()
                {
                    RezervacijaID = r.RezervacijaID,
                    KorisnikID = r.KorisnikID,
                    RedVoznjeID = r.RedVoznjeID,
                    Datum = r.Datum,
                    Vreme = r.Vreme
                };

                rezervacije.Add(rezervacijaBO);
            }

            return rezervacije;
        }

        public IEnumerable<RezervacijaBO> PrikazRezervacije()
        {
            List<RezervacijaBO> rezervacije = new List<RezervacijaBO>();

            string email = System.Web.HttpContext.Current.User.Identity.Name;

            int korisnikID = stanica.Korisnik.Where(t => t.Email == email).Select(t => t.KorisnikID).FirstOrDefault();

            foreach (Rezervacija r in stanica.Rezervacija.Where(t=> t.KorisnikID == korisnikID))
            {
                RezervacijaBO rez = new RezervacijaBO
                {
                    RezervacijaID = r.RezervacijaID,
                    KorisnikID = korisnikID,
                    RedVoznjeID = r.RedVoznjeID,
                    Datum = r.Datum,
                    Vreme = r.Vreme
                };

                rezervacije.Add(rez);
            }

            return rezervacije;

        }




    }
}
