using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class FechasFacturacionController : Controller
    {
        [Authorize]
        public ActionResult FechasFacturacion()
        {
            return View();
        }
    }
}