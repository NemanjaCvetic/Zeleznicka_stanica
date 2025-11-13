using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zeleznicka_stanica.Models.Interfaces;

namespace Zeleznicka_stanica.Models.EFRepository
{
    public class AuthRepository : IAuthRepository
    {
        private ZeleznickaStanicaEntities stanica = new ZeleznickaStanicaEntities();
       public void AddUser(KorisnikBO korisnik)
        {
            if (IsValid(korisnik)) return;

            Korisnik k = new Korisnik()
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Sifra = korisnik.Sifra,
                Email = korisnik.Email
            };

            if (korisnik.Status == null)
            {
                k.Status = "putnik";
            }
            else
                k.Status = korisnik.Status;

            try
            {
                stanica.Korisnik.Add(k);
                stanica.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greska " + ex.Message);
            }

        }

        public List<string> GetRolesForUser(string username)
        {
            Korisnik k = stanica.Korisnik.FirstOrDefault(t => t.Email == username);

            var userRoles = (from kor in stanica.Korisnik
                             where kor.Email == username
                             select kor.Status).ToList();
            return userRoles;
        }

        public bool IsValid(KorisnikBO korisnik)
        {
            bool isValid = false;
            if (stanica.Korisnik.Any(t => t.Email == korisnik.Email && t.Sifra == korisnik.Sifra))
                isValid = true;

            else if (stanica.Korisnik.Any(t => t.Email == korisnik.Email && t.Sifra != korisnik.Sifra))
                isValid = false;

            

            return isValid;
        }
    }
}