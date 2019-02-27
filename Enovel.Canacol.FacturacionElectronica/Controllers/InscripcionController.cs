using Enovel.Canacol.FacturacionElectronica.Models;
using System.Linq;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class InscripcionController : Controller
    {

        public string CurrentController
        {
            get
            {
                return this.ControllerContext.RouteData.Values["controller"].ToString();
            }
        }

        // GET: Inscripcion
        public ActionResult Inscripcion()
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
            var taxqualities = entities.tblCalidadTributaria.ToList();
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
            ViewBag.taxqualitiesListName = taxqualitiesList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblUsuariosProveedor userModel)
        {
            using (bdFacturacionElectronicaEntities db = new bdFacturacionElectronicaEntities())
            {
                string prueba = "";
                return RedirectToAction("Index", "Login");
            }


        }
    }
}