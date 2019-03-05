using System;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class FechasFacturacionController : Controller
    {
        // GET: FechasFacturacion
        public ActionResult GestionarFechas()
        {
            string PeriodoActual = DateTime.Now.Year.ToString();
            ViewBag.PeriodoActual = PeriodoActual;
            return View();
        }
        
    }
}