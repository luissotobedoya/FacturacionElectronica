
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult http404()
        {
            return View();
        }

        public ActionResult http405()
        {
            return View();
        }

        public ActionResult general()
        {
            return View();
        }
    }
}