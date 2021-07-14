using System;
using System.Collections.Generic;

namespace kalum2021.Models
{
    public class Seminarios
    {
        public string SeminarioId { get; set; }
        public string NombreSeminario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string ModuloId {get;set;}
        public virtual Modulos Modulos {get;set;}
        public virtual List<DetalleActividades> DetalleActividades {get;set;}
        public Seminarios()
        {

        }

        public override string ToString()
        {
            return this.NombreSeminario;
        }
    }
}