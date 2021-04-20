
using System;

namespace Models
{
    public abstract class Person
    {

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public Person()
        {

        }
        public Person(string Nombres, string Apellidos, string Email, DateTime Birthday, string Gender, string Phone)
        {
            this.Nombres = Nombres;
            this.Apellidos = Apellidos;
            this.Email = Email;
            this.Birthday = Birthday;
            this.Gender = Gender;
            this.Phone = Phone;
        }
        public Person(string Nombres, string Apellidos, string Email)
        {
            this.Nombres = Nombres;
            this.Apellidos = Apellidos;
            this.Email = Email;
        }
        


    }
}