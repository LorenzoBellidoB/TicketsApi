﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENT
{
    [Table("empresas")]
    public class clsEmpresa : SoftDeletableEntity
    {
        #region Propiedades
        [Key]
        [Column("id")]
        public int IdEmpresa { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("cif")]
        public string Cif {  get; set; }
        [Column("calle")]
        public string Calle { get; set; }
        [Column("codigo_postal")]
        public string Codigo_postal { get; set; }
        [Column("localidad")]
        public string Localidad { get; set; }
        [Column("provincia")]
        public string Provincia { get; set; }
        [Column("telefono")]
        public string Telefono { get; set; }
        [Column("correo")]
        public string Correo { get; set; }
        [Column("direccion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Direccion { get; set; }


        #endregion

        #region Constructores

        public clsEmpresa()
        {
        }

        public clsEmpresa(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        public clsEmpresa( string nombre, string cif, string calle, string codigo_postal, string localidad, string provincia, string direccion, string telefono, string correo)
        {
            Nombre = nombre;
            Cif = cif;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
        }

        public clsEmpresa(int idEmpresa, string nombre, string cif, string calle, string codigo_postal, string localidad, string provincia, string telefono, string correo)
        {
            IdEmpresa = idEmpresa;
            Nombre = nombre;
            Cif = cif;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Telefono = telefono;
            Correo = correo;
        }

        public clsEmpresa(int idEmpresa, string nombre, string cif, string calle, string codigo_postal, string localidad, string provincia, string direccion, string telefono, string correo)
        {
            IdEmpresa = idEmpresa;
            Nombre = nombre;
            Cif = cif;
            Calle = calle;
            Codigo_postal = codigo_postal;
            Localidad = localidad;
            Provincia = provincia;
            Direccion = direccion;
            Telefono = telefono;
            Correo = correo;
        }

        #endregion
    }
}
