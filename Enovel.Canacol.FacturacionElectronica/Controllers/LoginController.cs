using Enovel.Canacol.FacturacionElectronica.Models;
using Enovel.Canacol.FacturacionElectronica.Models.LoginModel;
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
        #endregion

        #region actions providers
        [HttpPost]
        public ActionResult AuthorizeProvider(LoginModelView userModel)
        {
            bdFacturacionElectronicaEntitiesModel entities = new bdFacturacionElectronicaEntitiesModel();
            int? userId = entities.ValidateUser(userModel.providerModel.UsuarioNit, userModel.providerModel.Password).FirstOrDefault();
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
            userModel.providerModel.LoginErrorMessage = message;
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
    }
}