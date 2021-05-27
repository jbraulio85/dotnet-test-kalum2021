namespace kalum2021.Models
{
    public class CarrerasTecnicas
    {
        public string CodigoCarrera {get;set;}
        public string NombreCarrera{get;set;}

        public CarrerasTecnicas()
        {

        }

        public CarrerasTecnicas(string CodigoCarrera, string NombreCarrera)
        {
            this.CodigoCarrera = CodigoCarrera;
            this.NombreCarrera = NombreCarrera;
        }
    }
}