﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("proveedores")]
    public class clsProveedor : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdProveedor { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("cif")]
        public string Cif { get; set; }
        [Column("direccion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Direccion { get; set; }
        [Column("telefono")]
        public string Telefono { get; set; }
        [Column("correo")]
        public string Correo { get; set; }
        [Column("calle")]
        public string Calle { get; set; }
        [Column("codigo_postal")]
        public string Codigo_postal { get; set; }
        [Column("localidad")]
        public string Localidad { get; set; }
        [Column("provincia")]
        public string Provincia { get; set; }
        [Column("empresa_id")]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual clsEmpresa Empresa { get; set; }

        #endregion

        #region Constructores
        public clsProveedor()
        {
        }
        public clsProveedor(int idProveedor)
        {
            IdProveedor = idProveedor;
        }

        public clsProveedor(int idProveedor, string nombre, string cif, string telefono, string correo, string calle, string codigo_postal, string localidad, string provincia, int idEmpresa)
        {
            IdProveedor = idProveedor;
            Nombre = nombre;
            Cif = cif;
            Telefono = telefono;
            Correo = correo;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            IdEmpresa = idEmpresa;
        }
        public clsProveedor(int idProveedor, string nombre, string cif, string direccion, string telefono, string correo, string calle, string codigo_postal, string localidad, string provincia, int idEmpresa)
        {
            IdProveedor = idProveedor;
            Nombre = nombre;
            Cif = cif;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            IdEmpresa = idEmpresa;
        }
        #endregion
    }
}
