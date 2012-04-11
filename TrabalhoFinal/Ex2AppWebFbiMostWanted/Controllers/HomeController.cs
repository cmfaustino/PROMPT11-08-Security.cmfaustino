using System.Web.Mvc;
using Ex2AppWebFbiMostWanted.Models;

namespace Ex2AppWebFbiMostWanted.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // HACK: alteracao de titulo
            ViewBag.Message = "Welcome to ASP.NET MVC! - Exerc. Final 2 - PROMPT 2011 uc08 - cmfaustino";

            return View();
        }

        // HACK: utilizacao de RequireClaimsAttribute que foi definido

        //[RequireClaims(EmailDomainsAllowed = new[] { "gmail.com", "yahoo.com" })
        //] // QUALQUER UM de 2 dominios
        //[RequireClaims(RolesAllowed = new[] { "admin" })] // OBTIDO COM carlos_bat_faustino@hotmail.com
        [RequireClaims(EmailDomainsAllowed = new[] { "hotmail.com" },
                            NamesAllowed = new[] { "Pedro Félix", "Carlos Faustino" })
        ] // EmailDomainsAllowed , NamesAllowed: 2 condicoes AO MESMO TEMPO, QUALQUER UM de 2 nomes
        public ActionResult About()
        {
            return View();
        }
    }
}
