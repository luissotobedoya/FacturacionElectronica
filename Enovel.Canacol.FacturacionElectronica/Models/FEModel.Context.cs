﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class bdFacturacionElectronicaEntitiesModel : DbContext
    {
        public bdFacturacionElectronicaEntitiesModel()
            : base("name=bdFacturacionElectronicaEntitiesModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCalidadTributaria> tblCalidadTributarias { get; set; }
        public virtual DbSet<tblEmpresa> tblEmpresas { get; set; }
        public virtual DbSet<tblEmpresaPorProveedor> tblEmpresaPorProveedors { get; set; }
        public virtual DbSet<tblFechasFacturacion> tblFechasFacturacions { get; set; }
        public virtual DbSet<tblGoodReceiptProveedor> tblGoodReceiptProveedors { get; set; }
        public virtual DbSet<tblGoodReceiptRadicado> tblGoodReceiptRadicadoes { get; set; }
        public virtual DbSet<tblLogsFuncionario> tblLogsFuncionarios { get; set; }
        public virtual DbSet<tblLogsProveedor> tblLogsProveedors { get; set; }
        public virtual DbSet<tblNumeroOrdenProveedor> tblNumeroOrdenProveedors { get; set; }
        public virtual DbSet<tblProceso> tblProcesoes { get; set; }
        public virtual DbSet<tblProveedor> tblProveedors { get; set; }
        public virtual DbSet<tblRadicado> tblRadicadoes { get; set; }
        public virtual DbSet<tblRole> tblRoles { get; set; }
        public virtual DbSet<tblRolesPorProceso> tblRolesPorProcesoes { get; set; }
        public virtual DbSet<tblRolesPorUsuario> tblRolesPorUsuarios { get; set; }
        public virtual DbSet<tblSoporte> tblSoportes { get; set; }
        public virtual DbSet<tblTipoOrden> tblTipoOrdens { get; set; }
        public virtual DbSet<tblUsuariosProveedor> tblUsuariosProveedors { get; set; }
        public virtual DbSet<UsuarioActivacion> UsuarioActivacions { get; set; }
        public virtual DbSet<tblCamaraComercio> tblCamaraComercios { get; set; }
        public virtual DbSet<tblRut> tblRuts { get; set; }
    
        public virtual ObjectResult<Nullable<int>> Validate_User(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Validate_User", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> ValidateUser(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("ValidateUser", usernameParameter, passwordParameter);
        }
    }
}
