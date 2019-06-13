using System;

namespace Models
{
    public class User
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }
        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public string City { get; }
        public string PostalCode { get; }
        public string EmailAddress { get; }
        public DateTime BirthDate { get; }
        public Gender UserGender { get; }
        public AccountType UserAccountType { get; }
        public bool Status { get; set; }
        public string Password { get; set; }
        public User Professional { get; set; }

        protected User(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        protected User(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
        }

        protected User(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime birthDate, Gender userGender, bool status, AccountType accountType, string password, User professional)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            PostalCode = postalCode;
            EmailAddress = emailAddress;
            BirthDate = birthDate;
            UserGender = userGender;
            Status = status;
            UserAccountType = accountType;
            Password = password;
            Professional = professional;
        }
    }
}
