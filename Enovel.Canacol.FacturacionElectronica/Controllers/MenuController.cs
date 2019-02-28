using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult MenuProveedor()
        {
            return View();
        }

        public ActionResult MenuFuncionario()
        {
            return View();
        }
    }
}