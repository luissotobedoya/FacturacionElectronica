using Enovel.Canacol.FacturacionElectronica.Models;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
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
            var taxqualities = entities.tblCalidadTributaria.ToList();
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
            ViewBag.taxqualitiesListName = taxqualitiesList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblUsuariosProveedor userModel)
        {
            string message = string.Empty;
            BindTaxQuality(userModel);
            if (userModel.Password.Equals(userModel.ConfirmarPassword))
            {
                int statusUser = validateUser(userModel);
                switch (statusUser)
                {
                    case 2:
                        GenerateAlert("error", "No eres proveedor activo", "Usuario no existe en la tabla de proveedores activos de Canacol, comuniquese al XXXXX-XXXXX");
                        break;
                    case 1:
                        GenerateAlert("error", "Usuario ya existe", "El usuario que intenta inscribir ya fue registrado anteriormente en el sistema de Facturación electrónica");
                        break;
                    case 0:
                        AddProviderUser(userModel);
                        break;
                }
            }
            else
            {
                GenerateAlert("error", "claves incorrectas", "Error en las claves','Las claves no coinciden, verifique de nuevo");
            }

            return View(CurrentController, userModel);
        }

        private void AddProviderUser(tblUsuariosProveedor userModel)
        {
            try
            {
                bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
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

        private void BindTaxQuality(tblUsuariosProveedor userModel)
        {
            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
            var taxqualities = entities.tblCalidadTributaria.ToList().Where(t => t.ID == userModel.IDCalidadTributaria);
            SelectList taxqualitiesList = new SelectList(taxqualities, "ID", "Nombre");
            ViewBag.taxqualitiesListName = taxqualitiesList;
        }


        public int validateUser(tblUsuariosProveedor userModel)
        {
            //------------------------------------------------------------------------------
            //     Status 2 = No existe en la tabla de proveedores activos
            //     Status 1 = El usuario ya existe en la tabla de usuarios
            //     Status 0 = El usuario no está registrado
            //------------------------------------------------------------------------------

            bdFacturacionElectronicaEntities entities = new bdFacturacionElectronicaEntities();
            var existUser = entities.tblUsuariosProveedor.Where(u => u.UsuarioNit == userModel.UsuarioNit).FirstOrDefault();
            if (existUser != null)
            {
                return 1;
            }
            else
            {
                var IsActiveProvider = entities.tblProveedor.Where(p => p.Nit == userModel.UsuarioNit).FirstOrDefault();
                return (IsActiveProvider != null) ? 0 : 2;
            }
        }
    }
}