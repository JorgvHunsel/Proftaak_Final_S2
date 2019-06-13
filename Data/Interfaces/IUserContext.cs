using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IUserContext
    {
        void AddNewUser(User newUser);
        List<User> GetAllUsers();
        int GetUserId(string firstName);
        User CheckValidityUser(string email, string password);
        User GetUserInfo(string email);
        void EditUser(User currentUser, string password);
        bool CheckIfUserAlreadyExists(string email);
        bool IsEmailValid(string email);
        bool CheckIfAccountIsActive(string email);
        User GetUserById(int userId);
        List<User> GetAllProfessionals();
        void LinkCareToProf(int careId, int profId);
    }
}
