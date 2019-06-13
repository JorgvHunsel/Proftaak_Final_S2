using Models;

namespace ProftaakASP_S2.Models
{
    public class UserViewModel
    {
        public enum AccountType { CareRecipient, Volunteer, Professional, Admin }
        public enum Gender { Man, Vrouw, Anders }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string EmailAddress { get; set; }
        public string BirthDate { get; set; }
        public string UserGender { get; set; }
        public User.AccountType UserAccountType { get; set; }
        public bool Status { get; set; }
        public User Professional { get; set; }

        public UserViewModel(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            City = user.City;
            PostalCode = user.PostalCode;
            EmailAddress = user.EmailAddress;
            BirthDate = user.BirthDate.ToShortDateString();
            UserGender = user.UserGender.ToString();
            UserAccountType = user.UserAccountType;
            Status = user.Status;
            Professional = user.Professional;
        }

        public UserViewModel()
        {

        }
    }
}
