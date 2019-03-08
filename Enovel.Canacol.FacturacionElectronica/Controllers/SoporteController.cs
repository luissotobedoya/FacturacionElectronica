using Enovel.Canacol.FacturacionElectronica.Models;
using Enovel.Canacol.FacturacionElectronica.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class SoporteController : Controller
    {
        // GET: Soporte
        public ActionResult MisSoportes()
        {
            return View();
        }

        public ActionResult RadicarSoporte(RadicacionViewModel radicacionViewModel)
        {
            try
            {
                string userNitLogged = HttpContext.User.Identity.Name;
                using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
                {
                    LoadInfoProvider(radicacionViewModel, userNitLogged, entities);
                    LoadCompaniesByProvider(userNitLogged, entities);
                }
            }
            catch (DbEntityValidationException)
            {
                GenerateAlert("error", "error", "validación de modelo", string.Empty);
            }
            catch (Exception exception)
            {
                GenerateAlert("error", "error", exception.Message, string.Empty);
            }
            return View(radicacionViewModel);
        }

        private static void LoadInfoProvider(RadicacionViewModel radicacionViewModel, string userNitLogged, bdFacturacionElectronicaEntities entities)
        {
            tblUsuariosProveedor userProvider = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userNitLogged).FirstOrDefault();
            radicacionViewModel.UserNit = userProvider.UsuarioNit;
            radicacionViewModel.RazonSocial = userProvider.RazonSocial;
        }

        private void LoadCompaniesByProvider(string userNitLogged, bdFacturacionElectronicaEntities entities)
        {
            var companiesByProvider = (from empresaProveedor in entities.tblEmpresaPorProveedor
                                       join empresa in entities.tblEmpresa
                                       on empresaProveedor.IDEmpresa equals empresa.ID
                                       where empresaProveedor.UsuarioNit == userNitLogged
                                       select new
                                       {
                                           ID = empresaProveedor.IDEmpresa,
                                           Nombre = empresa.Nombre
                                       }).ToList();

            SelectList CompaniesList = new SelectList(companiesByProvider, "ID", "Nombre");
            ViewBag.companies = CompaniesList;
        }

        private void GenerateAlert(string type, string title, string message, string redirectPage)
        {
            switch (type.ToLower())
            {
                case "success":
                    TempData["alert"] = string.Format("<script>alertSuccess('{0}','{1}', '{2}')</script>", title, message, redirectPage);
                    break;
                case "error":
                    TempData["alert"] = string.Format("<script>alertError('{0}','{1}')</script>", title, message);
                    break;
                case "info":
                    TempData["alert"] = string.Format("<script>alertInfo('{0}','{1}')</script>", title, message);
                    break;
            }
        }
    }
}