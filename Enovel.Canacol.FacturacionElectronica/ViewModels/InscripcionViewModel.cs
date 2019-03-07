using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enovel.Canacol.FacturacionElectronica.ViewModels
{
    public class InscripcionViewModel
    {
        public int ID { get; set; }

        [DisplayName("NIT")]
        [Required(ErrorMessage = "El usuario es requerido")]
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

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "RUT")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf)$", ErrorMessage = "Sólo se permiten archivos con extensión PDF")]
        public string Rut { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cámara de comercio")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf)$", ErrorMessage = "Sólo se permiten archivos con extensión PDF")]
        public string CamaraComercio { get; set; }

        [DisplayName("Clave")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La clave debe contener por lo mínimo 5 y máximo 10 caracteres(combinación de letras y números)")]
        [Required(ErrorMessage = "La clave es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirmar clave")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La clave debe contener por lo mínimo 5 y máximo 10 caracteres (combinación de letras y números)")]
        [Required(ErrorMessage = "La clave es requerida")]
        [DataType(DataType.Password)]
        public string ConfirmarPassword { get; set; }
    }
}