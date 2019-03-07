using Enovel.Canacol.FacturacionElectronica.Models;
using Enovel.Canacol.FacturacionElectronica.ViewModels;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class InscripcionController : Controller
    {

        #region Properties
        public string CurrentController
        {
            get
            {
                return ControllerContext.RouteData.Values["controller"].ToString();
            }
        }

        #endregion

        public ActionResult Inscripcion()
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                var taxqualities = entities.tblCalidadTributaria.ToList();
                SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
                ViewBag.taxqualitiesListName = taxqualitiesList;
                return View();
            }
        }

        [HttpPost]
        public JsonResult ValidateNitProvider(string providerNit)
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                var provider = entities.tblProveedor.Where(p => p.Nit.ToLower() == providerNit.ToLower()).FirstOrDefault();
                return Json(provider, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Create(InscripcionViewModel inscripcionViewModel)
        {
            BindTaxQuality(inscripcionViewModel);
            if (inscripcionViewModel.Password.Equals(inscripcionViewModel.ConfirmarPassword))
            {
                int statusUser = ValidateStatusUser(inscripcionViewModel);
                switch (statusUser)
                {
                    case 2:
                        GenerateAlert("error", "No eres proveedor activo", "Usuario no existe en la tabla de proveedores activos de Canacol, comuniquese al XXXXX-XXXXX", string.Empty);
                        break;
                    case 1:
                        GenerateAlert("error", "Usuario ya existe", "El usuario que intenta inscribir ya fue registrado anteriormente en el sistema de Facturación electrónica", string.Empty);
                        break;
                    case 0:
                        AddProviderUser(inscripcionViewModel);
                        break;
                }
            }
            else
            {
                GenerateAlert("error", "claves incorrectas", "Error en las claves','Las claves no coinciden, verifique de nuevo", string.Empty);
            }

            return View(CurrentController, inscripcionViewModel);
        }

        private void AddProviderUser(InscripcionViewModel inscripcionViewModel)
        {
            try
            {
                tblUsuariosProveedor providerUser = InsertUserProvider(inscripcionViewModel);
                UsuarioActivacion userActivation = InsertUserActivation(providerUser);
                SendActivationLinkEmail(providerUser, userActivation);
                GenerateAlert("success", "Inscripción éxitosa", "La inscripción se ha realizado correctamente, recuerde activar su cuenta mediante el link de activación que se le acaba de enviar al email", "/");
            }
            catch (DbEntityValidationException)
            {
                GenerateAlert("error", "error", "validación de modelo", string.Empty);
            }
            catch (Exception exception)
            {
                GenerateAlert("error", "error", exception.Message, string.Empty);
            }
        }

        private void SendActivationLinkEmail(tblUsuariosProveedor userModel, UsuarioActivacion userActivation)
        {
            string path = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            string activationLink = string.Format("{0}/Login/LinkActivacion/?userId={1}&&activactioncode={2}", path, userActivation.UsuarioID, userActivation.ActivacionCodigo);
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("test1@canacol.onmicrosoft.com", "Afes2018*#");
            MailAddress from = new MailAddress("test1@canacol.onmicrosoft.com", String.Empty, Encoding.UTF8);
            MailAddress to = new MailAddress(userModel.Email);
            MailMessage message = new MailMessage(from, to)
            {
                Body = string.Format("Cordial saludo. <br/> Este es el link de activación del usuario que acabas de registrar <b>{0}</b>, por favor haga clic en el siguiente link para poder activar su cuenta <a href='{1}' alt='link de activación'>ACTIVAR CUENTA</a>", userModel.UsuarioNit, activationLink),
                IsBodyHtml = true,
                Priority = MailPriority.High,
                BodyEncoding = Encoding.UTF8,
                Subject = "Link de activación Portal Proveedores - Facturación Electrónica",
                SubjectEncoding = Encoding.UTF8
            };
            client.Send(message);
        }

        private UsuarioActivacion InsertUserActivation(tblUsuariosProveedor userModel)
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                UsuarioActivacion userActivation = new UsuarioActivacion();
                userActivation.UsuarioID = userModel.ID;
                userActivation.ActivacionCodigo = Guid.NewGuid();
                entities.UsuarioActivacion.Add(userActivation);
                entities.SaveChanges();
                return userActivation;
            }
        }

        private tblUsuariosProveedor InsertUserProvider(InscripcionViewModel inscripcionViewModel)
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase fileRut = Request.Files["Rut"];
                HttpPostedFileBase fileCamaraComercio = Request.Files["CamaraComercio"];
                string locationRut = GetLocationFile(fileRut);
                string locationCamaraComercio = GetLocationFile(fileCamaraComercio);
                tblUsuariosProveedor providerUser = SetModelProviderUser(inscripcionViewModel, locationRut, locationCamaraComercio);
                entities.tblUsuariosProveedor.Add(providerUser);
                entities.SaveChanges();
                return providerUser;
            }
        }

        private tblUsuariosProveedor SetModelProviderUser(InscripcionViewModel inscripcionViewModel, string locationRut, string locationCamaraComercio)
        {
            tblUsuariosProveedor providerUser = new tblUsuariosProveedor();
            providerUser.UsuarioNit = inscripcionViewModel.UserNit.ToUpper();
            providerUser.Password = inscripcionViewModel.Password;
            providerUser.RazonSocial = inscripcionViewModel.RazonSocial.ToUpper();
            providerUser.IDCalidadTributaria = inscripcionViewModel.IDCalidadTributaria;
            providerUser.Email = inscripcionViewModel.Email.ToUpper();
            providerUser.Telefono = inscripcionViewModel.Telefono.ToUpper();
            providerUser.Direccion = inscripcionViewModel.Direccion.ToUpper();
            providerUser.RepresentanteLegal = inscripcionViewModel.RepresentanteLegal.ToUpper();
            providerUser.RutaRut = locationRut;
            providerUser.RutaCamaraComercio = locationCamaraComercio;
            providerUser.Estado = "ACTIVACIÓN";
            providerUser.CreatedDate = DateTime.Now;
            return providerUser;
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

        private string GetLocationFile(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            string location = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            file.SaveAs(location);
            return location;
        }

        private void BindTaxQuality(InscripcionViewModel inscripcionViewModel)
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                var taxqualities = entities.tblCalidadTributaria.ToList().Where(t => t.ID == inscripcionViewModel.IDCalidadTributaria);
                SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
                ViewBag.taxqualitiesListName = taxqualitiesList;
            }
        }

        public int ValidateStatusUser(InscripcionViewModel inscripcionViewModel)
        {
            //------------------------------------------------------------------------------
            //     Status 2 = No existe en la tabla de proveedores activos
            //     Status 1 = El usuario ya existe en la tabla de usuarios
            //     Status 0 = El usuario no está registrado
            //------------------------------------------------------------------------------

            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                var existUser = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == inscripcionViewModel.UserNit).FirstOrDefault();
                if (existUser != null)
                {
                    return 1;
                }
                else
                {
                    var IsActiveProvider = entities.tblProveedor.Where(p => p.Nit == inscripcionViewModel.UserNit).FirstOrDefault();
                    return (IsActiveProvider != null) ? 0 : 2;
                }
            }
        }
    }
}