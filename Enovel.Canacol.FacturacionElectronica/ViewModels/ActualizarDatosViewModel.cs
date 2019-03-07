using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enovel.Canacol.FacturacionElectronica.ViewModels
{
    public class ActualizarDatosViewModel
    {
        public int ID { get; set; }

        public string UserNit { get; set; }

        [DisplayName("Razón social")]
        [Required(ErrorMessage = "La razón social es requerida")]
        public string RazonSocial { get; set; }

        [DisplayName("Calidad Tributaria")]
        [Required(ErrorMessage = "La calidad tributaria es requerida")]
        public int IDCalidadTributaria { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "El email es requerido")]
        [RegularExpression("^[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*(\\.[a-zA-Z]{2,6})$", ErrorMessage = "Debe escribir un correo electrónico válido")]
        public string Email { get; set; }

        public string Emaildb { get; set; }

        [DisplayName("Teléfono")]
        [Range(0, int.MaxValue, ErrorMessage = "Sólo se permiten números positivos")]
        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Telefono { get; set; }

        [DisplayName("Dirección")]
        [Required(ErrorMessage = "La dirección es requerida")]
        public string Direccion { get; set; }

        [DisplayName("Representante Legal")]
        [Required(ErrorMessage = "El representante legal es requerido")]
        public string RepresentanteLegal { get; set; }

        [Display(Name = "Reemplazar RUT")]
        [RegularExpression(@"^.*\.(pdf|PDF)$", ErrorMessage = "Sólo se permiten archivos con extensión PDF")]
        public string Rut { get; set; }

        [Display(Name = "Reemplazar Cámara de comercio")]
        [RegularExpression(@"^.*\.(pdf|PDF)$", ErrorMessage = "Sólo se permiten archivos con extensión PDF")]
        public string CamaraComercio { get; set; }

    }
}