using System;
namespace Models
{
    public class Student : Person
    {
        public string StudentId { get; set; }
        public Student()
        {

        }
        public Student(string StudentId, string Nombres, string Apellidos, string Email, DateTime Birthday, string Gender, string Phone)
            : base(Nombres, Apellidos, Email, Birthday, Gender, Phone)
        {
            this.StudentId = StudentId;
        }


    }
}