using Enovel.Canacol.FacturacionElectronica.Models;
using Enovel.Canacol.FacturacionElectronica.Models.LoginModel;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class LoginController : Controller
    {
        public string CurrentController
        {
            get
            {
                return this.ControllerContext.RouteData.Values["controller"].ToString();
            }
        }

        #region vistas
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult InicioProveedor()
        {
            return View();
        }

        [Authorize]
        public ActionResult InicioFuncionario()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LinkActivacion(string userId, string activactionCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(activactionCode))
                {
                    bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
                    int userIdFind = int.Parse(userId);
                    Guid activactionCodeFind = new Guid(activactionCode);
                    var activationRegister = entities.UsuarioActivacion.Where(x => x.UsuarioID == userIdFind && x.ActivacionCodigo == activactionCodeFind).FirstOrDefault();
                    if (activationRegister != null)
                    {
                        ActivateUser(entities, userIdFind, activationRegister);
                    }
                    else
                    {
                        var userModel = entities.tblUsuariosProveedor.SingleOrDefault(u => u.ID == userIdFind);
                        if(userModel != null && userModel.Estado.ToLower().Equals("activo"))
                        {
                            GenerateAlert("info", "Usuario activo", "El usuario ya se encuentra activo en el sistema de Facturación electrónica", "/Login");
                        }
                    }
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            catch (Exception exception)
            {
                string errorMessage = exception.Message;
                return RedirectToAction("http404", "Error");
            }
        }
        #endregion

        #region actions providers
        [HttpPost]
        public ActionResult AuthorizeProvider(LoginModelView userModel)
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
            int? userId = entities.Validate_User(userModel.providerModel.UsuarioNit, userModel.providerModel.Password).FirstOrDefault();
            string message = string.Empty;
            switch (userId.Value)
            {
                case -1:
                    message = "Usuario o clave incorrecta";
                    break;
                case -2:
                    message = "La cuenta no ha sido activada";
                    break;
                default:
                    FormsAuthentication.SetAuthCookie(userModel.providerModel.UsuarioNit, true);
                    if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                    {
                        return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                    }
                    else
                    {
                        return RedirectToAction("MenuProveedor", "Menu");
                    }
            }

            ViewBag.Message = message;
            userModel.providerModel.FEErrorMessage = message;
            return View("Index", userModel);
        }

        #endregion

        #region actions functionary
        [HttpPost]
        public ActionResult AuthorizeFunctionary(LoginModelView userModel)
        {
            return View();
        }
        #endregion

        [HttpPost]
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }


        private void ActivateUser(bdFacturacionElectronicaEntities entities, int userIdFind, UsuarioActivacion activationRegister)
        {
            activationRegister = entities.UsuarioActivacion.Remove(activationRegister);
            if (activationRegister != null)
            {
                var userModel = entities.tblUsuariosProveedor.SingleOrDefault(u => u.ID == userIdFind);
                if (userModel != null)
                {
                    userModel.UsuarioNit = userModel.UsuarioNit.ToUpper();
                    userModel.Password = userModel.Password;
                    userModel.ConfirmarPassword = userModel.Password;
                    userModel.RazonSocial = userModel.RazonSocial.ToUpper();
                    userModel.IDCalidadTributaria = userModel.IDCalidadTributaria;
                    userModel.Email = userModel.Email.ToUpper();
                    userModel.Telefono = userModel.Telefono.ToUpper();
                    userModel.Direccion = userModel.Direccion.ToUpper();
                    userModel.RepresentanteLegal = userModel.RepresentanteLegal.ToUpper();
                    userModel.RutaRut = userModel.RutaRut;
                    userModel.RutaCamaraComercio = userModel.RutaCamaraComercio;
                    userModel.Estado = "ACTIVO";
                }
                entities.SaveChanges();
                GenerateAlert("success", "Activación de cuenta", "Su cuenta ha sido activada correctamente, ya puede iniciar sesión", "/");
            }
        }

        private void GenerateAlert(string type, string title, string message, string redirectPage)
        {
            switch (type.ToLower())
            {
                case "success":
                    TempData["alert"] = string.Format("<script>alertSuccess('{0}','{1}','{2}')</script>", title, message, redirectPage);
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