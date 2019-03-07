namespace Enovel.Canacol.FacturacionElectronica.Models
{
    using System;
    using System.Collections.Generic;

    public partial class tblUsuariosProveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUsuariosProveedor()
        {
            this.tblCamaraComercio = new HashSet<tblCamaraComercio>();
            this.tblRadicado = new HashSet<tblRadicado>();
            this.tblRut = new HashSet<tblRut>();
            this.UsuarioActivacion = new HashSet<UsuarioActivacion>();
        }

        public int ID { get; set; }
        public string UsuarioNit { get; set; }
        public string Password { get; set; }
        public string RazonSocial { get; set; }
        public Nullable<int> IDCalidadTributaria { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string RepresentanteLegal { get; set; }
        public string RutaRut { get; set; }
        public string RutaCamaraComercio { get; set; }
        public string Estado { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public string FEErrorMessage { get; set; }
        public virtual tblCalidadTributaria tblCalidadTributaria { get; set; }
        public virtual tblEmpresa tblEmpresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRadicado> tblRadicado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioActivacion> UsuarioActivacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCamaraComercio> tblCamaraComercio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRut> tblRut { get; set; }
    }
}
