namespace kalum2021.Models
{
    public class UsuariosApp
    {
        public int Id {get;set;}
        public string Username {get;set;}
        public string Password {get;set;}
        public string Nombres {get;set;}
        public string Apellidos {get; set;}
        public string Email {get;set;}

        public UsuariosApp ()
        {

        }

        public UsuariosApp(int Id, string Username, string Password, string Nombres, string Apellidos, string Email)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.Nombres = Nombres;
            this.Apellidos = Apellidos;
            this.Email = Email;
        }
    }
}