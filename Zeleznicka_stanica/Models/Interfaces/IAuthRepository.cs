using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeleznicka_stanica.Models.Interfaces
{
    interface IAuthRepository
    {
        bool IsValid(KorisnikBO korisnik);

        void AddUser(KorisnikBO korisnik);

        List<string> GetRolesForUser(string username);
    }
}
