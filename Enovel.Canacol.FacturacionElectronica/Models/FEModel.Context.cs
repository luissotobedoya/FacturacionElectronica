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
    
    public partial class bdFacturacionElectronicaEntities : DbContext
    {
        public bdFacturacionElectronicaEntities()
            : base("name=bdFacturacionElectronicaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCalidadTributaria> tblCalidadTributaria { get; set; }
        public virtual DbSet<tblEmpresa> tblEmpresa { get; set; }
        public virtual DbSet<tblEmpresaPorProveedor> tblEmpresaPorProveedor { get; set; }
        public virtual DbSet<tblFechasFacturacion> tblFechasFacturacion { get; set; }
        public virtual DbSet<tblGoodReceiptProveedor> tblGoodReceiptProveedor { get; set; }
        public virtual DbSet<tblGoodReceiptRadicado> tblGoodReceiptRadicado { get; set; }
        public virtual DbSet<tblLogsFuncionario> tblLogsFuncionario { get; set; }
        public virtual DbSet<tblLogsProveedor> tblLogsProveedor { get; set; }
        public virtual DbSet<tblProceso> tblProceso { get; set; }
        public virtual DbSet<tblProveedor> tblProveedor { get; set; }
        public virtual DbSet<tblRadicado> tblRadicado { get; set; }
        public virtual DbSet<tblRoles> tblRoles { get; set; }
        public virtual DbSet<tblRolesPorProceso> tblRolesPorProceso { get; set; }
        public virtual DbSet<tblRolesPorUsuario> tblRolesPorUsuario { get; set; }
        public virtual DbSet<tblSoporte> tblSoporte { get; set; }
        public virtual DbSet<tblTipoOrden> tblTipoOrden { get; set; }
        public virtual DbSet<tblUsuariosProveedor> tblUsuariosProveedor { get; set; }
        public virtual DbSet<UsuarioActivacion> UsuarioActivacion { get; set; }
        public virtual DbSet<tblNumeroOrdenProveedor> tblNumeroOrdenProveedor { get; set; }
    
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
