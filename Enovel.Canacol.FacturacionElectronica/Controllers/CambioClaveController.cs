using Enovel.Canacol.FacturacionElectronica.Models;
using Enovel.Canacol.FacturacionElectronica.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class CambioClaveController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(CambioClaveViewModel cambioClaveViewModel)
        {
            try
            {
                string userNitLogged = HttpContext.User.Identity.Name;
                using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
                {
                    tblUsuariosProveedor userProvider = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userNitLogged).FirstOrDefault();
                    string keyUser = userProvider.Password;
                    if (keyUser.Equals(cambioClaveViewModel.PasswordActual))
                    {
                        if (cambioClaveViewModel.PasswordActual.Equals(cambioClaveViewModel.NuevaPassword))
                        {
                            GenerateAlert("error", "Clave nueva igual a la clave actual", "La clave nueva no puede ser igual a la clave actual", string.Empty);
                            return View("Index", cambioClaveViewModel);
                        }
                        else
                        {
                            if (cambioClaveViewModel.NuevaPassword.Equals(cambioClaveViewModel.ConfirmarNuevaPassword))
                            {
                                updatePassword(userProvider, cambioClaveViewModel, entities);
                                GenerateAlert("success", "Cambio de clave éxitosa", "La clave ha sido cambiada correctamente", "/Menu/MenuProveedor");
                            }
                            else
                            {
                                GenerateAlert("error", "Claves nuevas no coinciden", "Las nuevas claves no coinciden", string.Empty);
                            }
                        }
                    }
                    else
                    {
                        GenerateAlert("error", "Clave actual inválida", "La clave actual no coincide con la registrada en el sistema", string.Empty);
                    }
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

            return View("Index", cambioClaveViewModel);
        }

        private void updatePassword(tblUsuariosProveedor userProvider, CambioClaveViewModel cambioClaveViewModel, bdFacturacionElectronicaEntities entities)
        {
            userProvider.Password = cambioClaveViewModel.NuevaPassword;
            entities.SaveChanges();
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