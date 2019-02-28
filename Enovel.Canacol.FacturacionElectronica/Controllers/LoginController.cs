using Enovel.Canacol.FacturacionElectronica.Models;
using System.Linq;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InicioProveedor()
        {
            return View();
        }

        public ActionResult InicioFuncionario()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Authorize(tblUsuariosProveedor userModel)
        {
            if(userModel.TipoUsuario.ToLower() == "proveedor")
            {
                using (bdFacturacionElectronicaEntities db = new bdFacturacionElectronicaEntities())
                {
                    return LoginProveedor(userModel, db);
                }
            }
            else
            {
                return LoginFuncionario();
            }
        }

        private ActionResult LoginFuncionario()
        {
            return RedirectToAction("MenuFuncionario", "Menu");
        }

        private ActionResult LoginProveedor(tblUsuariosProveedor userModel, bdFacturacionElectronicaEntities db)
        {
            var user = db.tblUsuariosProveedor.Where(u => u.UsuarioNit.Equals(userModel.UsuarioNit) && u.Password.Equals(userModel.Password)).FirstOrDefault();
            if (user == null)
            {
                userModel.LoginErrorMessage = "Usuario o password inválido";
                return View("Index", userModel);
            }
            else
            {
                Session["UserID"] = user.ID;
                Session["Username"] = user.RazonSocial;
                return RedirectToAction("MenuProveedor", "Menu");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}