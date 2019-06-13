using System;

namespace Models
{
    public class Volunteer : User
    {
        public Volunteer(string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateTime, Gender userGender, bool status, AccountType accountType, string password) : base(firstName, lastName, address, city, postalCode, emailAddress, dateTime, userGender, status, accountType, password)
        {
        }

        public Volunteer(int userId, string firstName, string lastName, string address, string city, string postalCode, string emailAddress, DateTime dateBirth, Gender userGender, bool status, AccountType accountType, string password) : base(userId, firstName, lastName, address, city, postalCode, emailAddress, dateBirth, userGender, status, accountType, password)
        {
        }

    }
}
