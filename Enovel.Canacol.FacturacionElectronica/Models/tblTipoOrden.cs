//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enovel.Canacol.FacturacionElectronica.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblTipoOrden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTipoOrden()
        {
            this.tblNumeroOrdenProveedor = new HashSet<tblNumeroOrdenProveedor>();
        }
    
        public int ID { get; set; }
        public string Nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNumeroOrdenProveedor> tblNumeroOrdenProveedor { get; set; }
    }
}
