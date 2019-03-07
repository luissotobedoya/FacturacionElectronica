using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enovel.Canacol.FacturacionElectronica.ViewModels
{
    public class CambioClaveViewModel
    {
        public int UsuarioID { get; set; }

        [DisplayName("Clave Actual")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La clave debe contener por lo mínimo 5 y máximo 10 caracteres(combinación de letras y números)")]
        [Required(ErrorMessage = "La clave es requerida")]
        [DataType(DataType.Password)]
        public string PasswordActual { get; set; }

        [DisplayName("Nueva clave")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La clave debe contener por lo mínimo 5 y máximo 10 caracteres(combinación de letras y números)")]
        [Required(ErrorMessage = "La clave es requerida")]
        [DataType(DataType.Password)]
        public string NuevaPassword { get; set; }

        [DisplayName("Confirmar nueva clave")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "La clave debe contener por lo mínimo 5 y máximo 10 caracteres(combinación de letras y números)")]
        [Required(ErrorMessage = "La clave es requerida")]
        [DataType(DataType.Password)]
        public string ConfirmarNuevaPassword { get; set; }
    }
}