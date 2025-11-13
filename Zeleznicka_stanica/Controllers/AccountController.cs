using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zeleznicka_stanica.Models;
using Zeleznicka_stanica.Models.EFRepository;
using Zeleznicka_stanica.Models.Interfaces;

namespace Zeleznicka_stanica.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        private IAuthRepository authRepository = new AuthRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(KorisnikBO korisnik)
        {
            if (authRepository.IsValid(korisnik))
            {
                FormsAuthentication.SetAuthCookie(korisnik.Email, false);
                return RedirectToAction("Pocetna", "Korisnik");
            }

            ModelState.AddModelError("", "Netacan username ili password");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(KorisnikBO korisnikBO)
        {
            if (authRepository.IsValid(korisnikBO))
            {
                ModelState.AddModelError("", "Nije moguce napraviti nalog sa unetim parametrima");
                return View();
            }

            //if (korisnik.IsUsernameTaken(korisnikBO))
            //{

            //    ModelState.AddModelError(string.Empty, "Korisnik sa unetim imenom vec postoji");
            //    return View();
            //}


            else if (ModelState.IsValid)
            {
                authRepository.AddUser(korisnikBO);

            }
            return RedirectToAction("Login");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}