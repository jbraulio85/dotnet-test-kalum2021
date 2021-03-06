
using System;

namespace Models
{
    public abstract class Person
    {

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;
        private string _gender;
        private string _phone;
        public Person()
        {

        }
        public Person(string FirstName, string LastName, string Email, DateTime Birthday, string Gender, string Phone)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Birthday = Birthday;
            this.Gender = Gender;
            this.Phone = Phone;
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }
            set
            {
                _birthday = value;
            }
        }
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

    }
}