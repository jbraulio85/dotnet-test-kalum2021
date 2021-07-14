using System;

namespace kalum2021.Models
{
    public class DetalleActividades
    {
        public string DetalleActividadId {get;set;}
        public string NombreActividad {get;set;}
        public int NotaActividad {get;set;}
        public DateTime FechaCreacion {get;set;}
        public DateTime FechaEntrega {get;set;}
        public DateTime FechaPostergacion {get;set;}
        public string Estado {get;set;}
        public string SeminarioId {get;set;}
        public virtual Seminarios Seminarios {get;set;}

        public DetalleActividades()
        {

        }

        public override string ToString()
        {
            return this.NombreActividad;
        }

    }
}