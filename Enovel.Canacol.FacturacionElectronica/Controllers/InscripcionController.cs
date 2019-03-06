using Enovel.Canacol.FacturacionElectronica.Models;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
<<<<<<< HEAD
=======
using System.Net;
using System.Net.Mail;
using System.Text;
>>>>>>> master
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

        public ActionResult Inscripcion()
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
<<<<<<< HEAD
            var taxqualities = entities.tblCalidadTributaria.ToList();
=======

            var taxqualities = entities.tblCalidadTributaria.ToList();

>>>>>>> master
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");

            ViewBag.taxqualitiesListName = taxqualitiesList;

            return View();
        }

        [HttpPost]
        public JsonResult ValidateNitProvider(string providerNit)
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
            var provider = entities.tblProveedor.Where(p => p.Nit.ToLower() == providerNit.ToLower()).FirstOrDefault();
            return Json(provider, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(tblUsuariosProveedor userModel)
        {
<<<<<<< HEAD
            string message = string.Empty;
=======
>>>>>>> master
            BindTaxQuality(userModel);
            if (userModel.Password.Equals(userModel.ConfirmarPassword))
            {
                int statusUser = ValidateStatusUser(userModel);

                switch (statusUser)
                {
                    case 2:
<<<<<<< HEAD
                        GenerateAlert("error", "No eres proveedor activo", "Usuario no existe en la tabla de proveedores activos de Canacol, comuniquese al XXXXX-XXXXX");
                        break;
                    case 1:
                        GenerateAlert("error", "Usuario ya existe", "El usuario que intenta inscribir ya fue registrado anteriormente en el sistema de Facturación electrónica");
=======
                        GenerateAlert("error", "No eres proveedor activo", "Usuario no existe en la tabla de proveedores activos de Canacol, comuniquese al XXXXX-XXXXX", string.Empty);
                        break;
                    case 1:
                        GenerateAlert("error", "Usuario ya existe", "El usuario que intenta inscribir ya fue registrado anteriormente en el sistema de Facturación electrónica", string.Empty);
>>>>>>> master
                        break;
                    case 0:
                        AddProviderUser(userModel);
                        break;
                }
            }
            else
            {
<<<<<<< HEAD
                GenerateAlert("error", "claves incorrectas", "Error en las claves','Las claves no coinciden, verifique de nuevo");
=======
                GenerateAlert("error", "claves incorrectas", "Error en las claves','Las claves no coinciden, verifique de nuevo", string.Empty);
>>>>>>> master
            }

            return View(CurrentController, userModel);
        }

        private void AddProviderUser(tblUsuariosProveedor userModel)
        {
            try
            {
                bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
<<<<<<< HEAD
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase fileRut = Request.Files["RutaRut"];
                HttpPostedFileBase fileCamaraComercio = Request.Files["RutaCamaraComercio"];
                string locationRut = getLocationFile(fileRut);
                string locationCamaraComercio = getLocationFile(fileCamaraComercio);

                entities.tblUsuariosProveedor.Add(new tblUsuariosProveedor
                {
                    UsuarioNit = userModel.UsuarioNit.ToUpper(),
                    Password = userModel.Password,
                    ConfirmarPassword = userModel.ConfirmarPassword,
                    RazonSocial = userModel.RazonSocial.ToUpper(),
                    IDCalidadTributaria = userModel.IDCalidadTributaria,
                    Email = userModel.Email.ToUpper(),
                    Telefono = userModel.Telefono.ToUpper(),
                    Direccion = userModel.Direccion.ToUpper(),
                    RepresentanteLegal = userModel.RepresentanteLegal.ToUpper(),
                    RutaRut = locationRut,
                    RutaCamaraComercio = locationCamaraComercio,
                    Estado = "ACTIVACIÓN",
                    CreatedDate = DateTime.Now,
                    FEErrorMessage = "Usuario creado correctamente"
                });
                entities.SaveChanges();

                GenerateAlert("success", "Inscripción éxitosa", "La inscripción se ha realizado correctamente, recuerde activar su cuenta mediante el link de activación que se le acaba de enviar al email");
            }
            catch (DbEntityValidationException dbException)
            {
=======

                InsertUserProvider(userModel, entities);

                UsuarioActivacion userActivation = InsertUserActivation(userModel, entities);

                SendActivationLinkEmail(userModel, userActivation);

                GenerateAlert("success", "Inscripción éxitosa", "La inscripción se ha realizado correctamente, recuerde activar su cuenta mediante el link de activación que se le acaba de enviar al email", "/");
            }
            catch (DbEntityValidationException dbException)
            {
            }
            catch (Exception exception)
            {
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

        private UsuarioActivacion InsertUserActivation(tblUsuariosProveedor userModel, bdFacturacionElectronicaEntities entities)
        {
            UsuarioActivacion userActivation = new UsuarioActivacion();

            userActivation.UsuarioID = userModel.ID;

            userActivation.ActivacionCodigo = Guid.NewGuid();

            entities.UsuarioActivacion.Add(userActivation);

            entities.SaveChanges();

            return userActivation;
        }

        private void InsertUserProvider(tblUsuariosProveedor userModel, bdFacturacionElectronicaEntities entities)
        {
            HttpFileCollectionBase files = Request.Files;

            HttpPostedFileBase fileRut = Request.Files["RutaRut"];

            HttpPostedFileBase fileCamaraComercio = Request.Files["RutaCamaraComercio"];

            string locationRut = GetLocationFile(fileRut);

            string locationCamaraComercio = GetLocationFile(fileCamaraComercio);

            UpdateUserModelProperties(userModel, locationRut, locationCamaraComercio);

            entities.tblUsuariosProveedor.Add(userModel);

            entities.SaveChanges();
        }

        private static void UpdateUserModelProperties(tblUsuariosProveedor userModel, string locationRut, string locationCamaraComercio)
        {
            userModel.UsuarioNit = userModel.UsuarioNit.ToUpper();

            userModel.Password = userModel.Password;

            userModel.ConfirmarPassword = userModel.ConfirmarPassword;

            userModel.RazonSocial = userModel.RazonSocial.ToUpper();

            userModel.IDCalidadTributaria = userModel.IDCalidadTributaria;

            userModel.Email = userModel.Email.ToUpper();

            userModel.Telefono = userModel.Telefono.ToUpper();

            userModel.Direccion = userModel.Direccion.ToUpper();

            userModel.RepresentanteLegal = userModel.RepresentanteLegal.ToUpper();

            userModel.RutaRut = locationRut;

            userModel.RutaCamaraComercio = locationCamaraComercio;

            userModel.Estado = "ACTIVACIÓN";

            userModel.CreatedDate = DateTime.Now;
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
>>>>>>> master
            }
            catch (Exception exception)
            {
            }
        }

        private void GenerateAlert(string type, string title, string message)
        {
            switch (type.ToLower())
            {
                case "success":
                    TempData["alert"] = string.Format("<script>alertInscripcionSucces('{0}','{1}')</script>", title, message);
                    break;
                case "error":
                    TempData["alert"] = string.Format("<script>alertError('{0}','{1}')</script>", title, message);
                    break;
                case "info":
                    TempData["alert"] = string.Format("<script>alertInfo('{0}','{1}')</script>", title, message);
                    break;
            }

        }

        private string getLocationFile(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            string location = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            file.SaveAs(location);
            return location;
        }

        private string GetLocationFile(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);

            string location = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

            file.SaveAs(location);

            return location;
        }

        private void BindTaxQuality(tblUsuariosProveedor userModel)
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
<<<<<<< HEAD
            var taxqualities = entities.tblCalidadTributaria.ToList().Where(t => t.ID == userModel.IDCalidadTributaria);
=======

            var taxqualities = entities.tblCalidadTributaria.ToList().Where(t => t.ID == userModel.IDCalidadTributaria);

>>>>>>> master
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");

            ViewBag.taxqualitiesListName = taxqualitiesList;
        }

        public int ValidateStatusUser(tblUsuariosProveedor userModel)
        {
            //------------------------------------------------------------------------------
            //     Status 2 = No existe en la tabla de proveedores activos
            //     Status 1 = El usuario ya existe en la tabla de usuarios
            //     Status 0 = El usuario no está registrado
            //------------------------------------------------------------------------------

            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
<<<<<<< HEAD
            var existUser = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userModel.UsuarioNit).FirstOrDefault();
=======

            var existUser = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userModel.UsuarioNit).FirstOrDefault();

>>>>>>> master
            if (existUser != null)
            {
                return 1;
            }
            else
            {
                var IsActiveProvider = entities.tblProveedor.Where(p => p.Nit == userModel.UsuarioNit).FirstOrDefault();
<<<<<<< HEAD
=======

>>>>>>> master
                return (IsActiveProvider != null) ? 0 : 2;
            }
        }
    }
}