using Enovel.Canacol.FacturacionElectronica.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            bdFacturacionElectronicaEntitiesModel entities = new bdFacturacionElectronicaEntitiesModel();
            var taxqualities = entities.tblCalidadTributarias.ToList();
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
            ViewBag.taxqualitiesListName = taxqualitiesList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblUsuariosProveedor userModel)
        {

            using (bdFacturacionElectronicaEntitiesModel db = new bdFacturacionElectronicaEntitiesModel())
            {

                return RedirectToAction(CurrentController, "Inscripcion");
            }
        }
    }
}