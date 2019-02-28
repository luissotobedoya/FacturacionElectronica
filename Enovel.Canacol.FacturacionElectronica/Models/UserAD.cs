using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Enovel.Canacol.FacturacionElectronica.Models
{
    public class UserAD
    {
        public string SID { get; set; }

        [DisplayName("Usuario")]
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Username { get; set; }

        [DisplayName("Clave")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La clave es requerida")]
        public string Password { get; set; }

        public string LoginErrorMessage { get; set; }

    }
}