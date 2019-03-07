namespace Enovel.Canacol.FacturacionElectronica.Models
{
    public partial class UsuarioActivacion
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public System.Guid ActivacionCodigo { get; set; }
    
        public virtual tblUsuariosProveedor tblUsuariosProveedor { get; set; }
    }
}
