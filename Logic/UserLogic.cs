using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class UserLogic
    {
        private readonly IUserContext _user;

        public UserLogic(IUserContext user)
        {
            _user = user;
        }

        public User CheckValidityUser(string email, string password)
        {
            if (email == "")
                throw new ArgumentException("Emailadress can't be empty");
            if (email.Length > 50)
                throw new ArgumentException("Emailadress can't be too long");

            if (password == "")
                throw new ArgumentException("Password can't be empty");
            if (password.Length > 50)
                throw new ArgumentException("Password can't be too long");


            return _user.CheckValidityUser(email, password);
        }

        public int GetUserId(string firstName)
        {
            return _user.GetUserId(firstName);
        }

        public List<User> GetAllUsers()
        {
            return _user.GetAllUsers();
        }

        public void AddNewUser(User newUser)
        {
            newUser.Password = Hasher.SecurePasswordHasher.Hash(newUser.Password);

            _user.AddNewUser(newUser);
        }

        public User GetCurrentUserInfo(string email)
        {
            return _user.GetUserInfo(email);
        }

        public void EditUser(User currentUser, string password)
        {
            _user.EditUser(currentUser, password);
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            return _user.CheckIfUserAlreadyExists(email);
        }

        public bool IsEmailValid(string email)
        {
            return _user.IsEmailValid(email);
        }

        public bool CheckIfAccountIsActive(string email)
        {
            return _user.CheckIfAccountIsActive(email);
        }

        public User GetUserById(int userId)
        {
            return _user.GetUserById(userId);
        }

        public bool SendEmailProfessional(string emailaddress)
        {
            try
            {
                MailAddress fromAddress = new MailAddress("maileye4participation@gmail.com", "NoReply Eye4Participation");
                MailAddress toAddress = new MailAddress(emailaddress);
                const string fromPassword = "Test1234!";
                const string subject = "New professional acocunt";
                const string body = "Hello there," +
                                    "" +
                                    "Je bent uitgenodigd om een professioneel hulpverlener account aan te maken." +
                                    "Dit kan je doen via de volgende link:" +
                                    "" +
                                    "Met vriendelijke groet," +
                                    "" +
                                    "Het administratorteam van Eye4Participation";

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<User> GetAllProfessionals()
        {
            return _user.GetAllProfessionals();
        }

        public void LinkCareToProf(int careId, int profId)
        {
            _user.LinkCareToProf(careId, profId);
        }
    }
}
