namespace kalum2021.Models
{
    public class Rol
    {
        public int RoleId {get;set;}
        public string RoleNombre {get;set;}
        public Rol ()
        {

        }

        public Rol (int RoleId,string RoleNombre)
        {
            this.RoleId = RoleId;
            this.RoleNombre = RoleNombre;
        }
    }
}