namespace kalum2021.Models
{
    public class Roles
    {
        public int RoleId {get;set;}
        public string RoleNombre {get;set;}

        public Roles ()
        {

        }

        public Roles (int RoleId, string RoleNombre)
        {
            this.RoleId = RoleId;
            this.RoleNombre = RoleNombre;
        }
    }
}