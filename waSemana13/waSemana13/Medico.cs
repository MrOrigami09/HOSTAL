//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace waSemana13
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medico
    {
        public int IdMedico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroColegiatura { get; set; }
        public string DNI { get; set; }
        public string Genero { get; set; }
        public int IdEspecialidad { get; set; }
        public int IdPais { get; set; }
    
        public virtual Especialidad Especialidad { get; set; }
        public virtual Pais Pais { get; set; }
    }
}
