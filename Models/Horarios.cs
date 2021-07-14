using System;
using System.Collections.Generic;

namespace kalum2021.Models
{
    public class Horarios
    {
        public string HorarioId {get;set;}
        public DateTime HorarioInicio {get; set;}
        public DateTime HorarioFinal {get;set;}
        public virtual List<Clases> Clases{get;set;}

        public Horarios ()
        {

        }

        public override string ToString()
        {
            return $"{this.HorarioInicio:hh\\:mm tt} - {this.HorarioFinal:hh\\:mm tt}";
        }
    }
}