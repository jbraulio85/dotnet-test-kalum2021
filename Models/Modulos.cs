namespace kalum2021.Models
{
    public class Modulos
    {
        public string ModuloId {get;set;}
        public string NombreModulo {get;set;}
        public int NumeroSeminarios {get;set;}
        public string CodigoCarrera {get;set;}
        public virtual CarrerasTecnicas CarrerasTecnicas {get;set;}

        public Modulos ()
        {

        }

        public override string ToString()
        {
            return this.NombreModulo;
        }
    }
}