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
using System.Web.Security;

namespace Enovel.Canacol.FacturacionElectronica.Controllers
{
    public class ActualizarDatosController : Controller
    {
        public ActionResult Index(ActualizarDatosViewModel actualizarDatosViewModel)
        {
            try
            {
                string userNitLogged = HttpContext.User.Identity.Name;
                using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
                {
                    LoadTaxQualities(entities);
                    DeleteModalStateErrors();
                    if (ModelState.IsValid)
                    {
                        LoadInfoUserProvider(actualizarDatosViewModel, userNitLogged, entities);
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

            return View(actualizarDatosViewModel);
        }

        private void DeleteModalStateErrors()
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }
        }

        private void LoadInfoUserProvider(ActualizarDatosViewModel actualizarDatosViewModel, string userNitLogged, bdFacturacionElectronicaEntities entities)
        {
            tblUsuariosProveedor userProvider = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userNitLogged).FirstOrDefault();
            actualizarDatosViewModel.UserNit = userProvider.UsuarioNit;
            actualizarDatosViewModel.RazonSocial = userProvider.RazonSocial;
            actualizarDatosViewModel.IDCalidadTributaria = (userProvider.IDCalidadTributaria.HasValue) ? userProvider.IDCalidadTributaria.Value : 0;
            actualizarDatosViewModel.Email = userProvider.Email;
            actualizarDatosViewModel.Emaildb = userProvider.Email;
            actualizarDatosViewModel.Telefono = userProvider.Telefono;
            actualizarDatosViewModel.Direccion = userProvider.Direccion;
            actualizarDatosViewModel.RepresentanteLegal = userProvider.RepresentanteLegal;
            actualizarDatosViewModel.Rut = GetUrlFile(userProvider.RutaRut);
            actualizarDatosViewModel.CamaraComercio = GetUrlFile(userProvider.RutaCamaraComercio);
        }

        private string GetUrlFile(string locationFile)
        {
            string urlfile = string.Empty;
            string path = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            int index = locationFile.IndexOf("DocumentsUpload");
            urlfile = string.Format("{0}/{1}", path, locationFile.Substring(index));
            return urlfile;
        }

        private void LoadTaxQualities(bdFacturacionElectronicaEntities entities)
        {
            var taxqualities = entities.tblCalidadTributaria.ToList();
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
            ViewBag.taxqualitiesListName = taxqualitiesList;
        }

        [HttpPost]
        public ActionResult Update(ActualizarDatosViewModel actualizarDatosViewModel)
        {
            try
            {
                BindTaxQuality(actualizarDatosViewModel);
                using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
                {
                    tblUsuariosProveedor userProvider = UpdateUserProvider(actualizarDatosViewModel, entities);

                    if (userProvider.Email.ToLower().Equals(actualizarDatosViewModel.Emaildb.ToLower()))
                    {
                        GenerateAlert("success", "Actualización éxitosa", "La actualización se ha realizado correctamente", "/Menu/MenuProveedor");
                    }
                    else
                    {
                        UsuarioActivacion userActivation = InsertUserActivation(userProvider);
                        SendActivationLinkEmail(userProvider, userActivation);
                        FormsAuthentication.SignOut();
                        GenerateAlert("success", "Actualización éxitosa", "La actualización se ha realizado correctamente, debido a que el email ha cambiado, se debe activar nuevamente la cuenta, verifique el link de activación que fue enviado al email.", "/");
                    }
                }
            }
            catch (DbEntityValidationException exception)
            {
                GenerateAlert("error", "error", "validación de modelo", string.Empty);
            }
            catch (Exception exception)
            {
                GenerateAlert("error", "error", exception.Message, string.Empty);
            }

            return View("Index", actualizarDatosViewModel);
        }

        private void BindTaxQuality(ActualizarDatosViewModel actualizarDatosViewModel)
        {
            using (bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities())
            {
                var taxqualities = entities.tblCalidadTributaria.ToList().Where(t => t.ID == actualizarDatosViewModel.IDCalidadTributaria);
                SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
                ViewBag.taxqualitiesListName = taxqualitiesList;
            }
        }

        private tblUsuariosProveedor UpdateUserProvider(ActualizarDatosViewModel actualizarDatosViewModel, bdFacturacionElectronicaEntities entities)
        {
            string userNit = HttpContext.User.Identity.Name;
            HttpFileCollectionBase files = Request.Files;
            tblUsuariosProveedor userProvider = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userNit).FirstOrDefault();
            userProvider.UsuarioNit = userNit;
            userProvider.RazonSocial = actualizarDatosViewModel.RazonSocial;
            userProvider.IDCalidadTributaria = actualizarDatosViewModel.IDCalidadTributaria;
            userProvider.Email = actualizarDatosViewModel.Email;
            userProvider.Telefono = actualizarDatosViewModel.Telefono;
            userProvider.Direccion = actualizarDatosViewModel.Direccion;
            userProvider.RepresentanteLegal = actualizarDatosViewModel.RepresentanteLegal;

            if (!string.IsNullOrEmpty(actualizarDatosViewModel.Rut))
            {
                HttpPostedFileBase fileRut = Request.Files["Rut"];
                string locationRut = GetLocationFile("RUT", actualizarDatosViewModel, fileRut);
                userProvider.RutaRut = locationRut;
            }

            if (!string.IsNullOrEmpty(actualizarDatosViewModel.CamaraComercio))
            {
                HttpPostedFileBase fileCamaraComercio = Request.Files["CamaraComercio"];
                string locationCamaraComercio = GetLocationFile("CAMARACOMERCIO", actualizarDatosViewModel, fileCamaraComercio);
                userProvider.RutaCamaraComercio = locationCamaraComercio;
            }

            entities.SaveChanges();
            return userProvider;
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

        private string GetLocationFile(string filetype, ActualizarDatosViewModel actualizarDatosViewModel, HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName);
            var fileName = string.Format("{0}_{1}.{2}", filetype, HttpContext.User.Identity.Name, extension);
            string location = Path.Combine(Server.MapPath("~/DocumentsUpload"), fileName);
            file.SaveAs(location);
            return location;
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
                Body = string.Format("Cordial saludo. <br/> Este es el link de activación del usuario que acabas de actualizar <b>{0}</b>, por favor haga clic en el siguiente link para poder activar su cuenta <a href='{1}' alt='link de activación'>ACTIVAR CUENTA</a>", userModel.UsuarioNit, activationLink),
                IsBodyHtml = true,
                Priority = MailPriority.High,
                BodyEncoding = Encoding.UTF8,
                Subject = "Link de activación Portal Proveedores - Facturación Electrónica",
                SubjectEncoding = Encoding.UTF8
            };
            client.Send(message);
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