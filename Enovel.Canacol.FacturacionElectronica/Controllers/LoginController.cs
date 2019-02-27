using Enovel.Canacol.FacturacionElectronica.Models;
using System.Linq;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(tblUsuariosProveedor userModel)
        {
            using (bdFacturacionElectronicaEntities db = new bdFacturacionElectronicaEntities())
            {
                var user = db.tblUsuariosProveedor.Where(u => u.UsuarioNit.Equals(userModel.UsuarioNit) && u.Password.Equals(userModel.Password)).FirstOrDefault();
                if(user == null)
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
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}