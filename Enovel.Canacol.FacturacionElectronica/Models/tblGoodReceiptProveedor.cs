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
    
    public partial class tblGoodReceiptProveedor
    {
        public int ID { get; set; }
        public int IDNumeroOrdenProveedor { get; set; }
        public string NumeroGoodReceipt { get; set; }
        public string TipoMoneda { get; set; }
        public Nullable<float> ValorGR { get; set; }
    
        public virtual tblGoodReceiptRadicado tblGoodReceiptRadicado { get; set; }
    }
}
