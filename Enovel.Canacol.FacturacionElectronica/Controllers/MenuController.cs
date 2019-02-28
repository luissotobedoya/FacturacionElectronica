using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class MenuController : Controller
    {
        [Authorize]
        public ActionResult MenuProveedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult MenuFuncionario()
        {
            return View();
        }
    }
}