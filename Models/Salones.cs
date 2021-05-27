namespace kalum2021.Models
{
    public class Salones
    {
        public string SalonId {get;set;}
        public int Capacidad {get;set;}
        public string Descripcion {get;set;}
        public string NombreSalon {get;set;}

        public Salones ()
        {

        }
        public Salones (string SalonId, int Capacidad, string Descripcion, string NombreSalon)
        {
            this.SalonId = SalonId;
            this.Capacidad = Capacidad;
            this.Descripcion = Descripcion;
            this.NombreSalon = NombreSalon;
        }
    }
}