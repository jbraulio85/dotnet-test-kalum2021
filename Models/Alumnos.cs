namespace kalum2021.Models
{
    public class Alumnos
    {
        public string Carne { get; set; }
        public string NoExpediente { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Email { get; set; }

        public Alumnos()
        {

        }
        public Alumnos(string NoExpedinete, string Nombres, string Apellidos, string Email)
        {
            this.NoExpediente = NoExpediente;
            this.Apellidos = Apellidos;
            this.Nombres = Nombres;
            this.Email = Email;
        }
        public Alumnos(string Carne, string NoExpedinete, string Nombres, string Apellidos, string Email)
        {
            this.Carne = Carne;
            this.NoExpediente = NoExpediente;
            this.Apellidos = Apellidos;
            this.Nombres = Nombres;
            this.Email = Email;
        }
    }
}