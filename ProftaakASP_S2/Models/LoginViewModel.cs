using System.ComponentModel.DataAnnotations;
using Models;

namespace ProftaakASP_S2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Het emailadres moet worden ingevuld")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Het wachtwoord moet worden ingevuld.")]
        public string Password { get; set; }

        public LoginViewModel(User user, string password)
        {
            EmailAddress = user.EmailAddress;
            Password = password;
        }

        public LoginViewModel()
        {
            
        }
    }
}

