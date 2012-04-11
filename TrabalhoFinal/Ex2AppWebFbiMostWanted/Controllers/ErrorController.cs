using System.Web.Mvc;

namespace Ex2AppWebFbiMostWanted.Controllers
{
    // HACK: criado controller para Erros, mas, mais especificamente, para erro HTTP 404 e HTTP 500

    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View("Error");

            // redireccionar para pagina inicial - GET: /Home/Index/
            //return RedirectToAction("Index", "Home");
        }

        // HACK: http://stackoverflow.com/questions/619895/how-can-i-properly-handle-404-in-asp-net-mvc

        // Estas operacoes acabaram por ser desnecessarias.

        //
        // GET: /Error/Http404

        public ActionResult Http404()
        {
            // mostrar respectiva View
            return View();
        }

        //
        // GET: /Error/Http500

        public ActionResult Http500()
        {
            // mostrar respectiva View
            return View();
        }

    }
}
