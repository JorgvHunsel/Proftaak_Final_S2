using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;

namespace ProftaakASP_S2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserLogic _userLogic;
        private readonly LogLogic _logLogic;

        public UserController(UserLogic userLogic, LogLogic logLogic)
        {
            _userLogic = userLogic;
            _logLogic = logLogic;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel userViewModel)
        {
            try
            {
                User newCustomer = _userLogic.CheckValidityUser(userViewModel.EmailAddress, userViewModel.Password);

                if (!newCustomer.Status)
                {
                    ViewBag.Message = "Dit account is geblokkeerd. Neem contact op met de administrator.";
                    return View();
                }

                
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, newCustomer.UserId.ToString()),
                    new Claim(ClaimTypes.Name, newCustomer.FirstName + " " + newCustomer.LastName),
                    new Claim(ClaimTypes.Gender, newCustomer.UserGender.ToString()),
                    new Claim(ClaimTypes.Email, newCustomer.EmailAddress),
                    new Claim(ClaimTypes.PostalCode, newCustomer.PostalCode),
                    new Claim(ClaimTypes.StreetAddress, newCustomer.Address),
                    new Claim(ClaimTypes.Role, newCustomer.UserAccountType.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                switch (newCustomer.UserAccountType)
                {
                    case global::Models.User.AccountType.Admin:
                        return RedirectToAction("QuestionOverview", "Admin");
                    case global::Models.User.AccountType.Volunteer:
                        return RedirectToAction("QuestionOverview", "Volunteer");
                    case global::Models.User.AccountType.Professional:
                        return RedirectToAction("QuestionOverview", "Professional");
                    default:
                        return RedirectToAction("Overview", "CareRecipient");
                }
            }
            catch (NullReferenceException)
            {
                ViewBag.Message = "De gegevens zijn niet ingevuld";
                return View();
            }
            catch(IndexOutOfRangeException)
            {
                ViewBag.Message = "De gegevens komen niet overeen";
                return View();
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Wachtwoord verkeerd ingevuld";
                return View();
            }

        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserViewModel userViewModel, string password, string passwordValidation)
        {
            try
            {
                if (password == passwordValidation)
                {
                    if (_userLogic.CheckIfUserAlreadyExists(userViewModel.EmailAddress))
                    {
                        if (_userLogic.IsEmailValid(userViewModel.EmailAddress))
                        {
                            switch (userViewModel.UserAccountType)
                            {
                                case global::Models.User.AccountType.CareRecipient:
                                    _userLogic.AddNewUser(new CareRecipient(userViewModel.FirstName, userViewModel.LastName,
                                        userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                                        userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                                        (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                        global::Models.User.AccountType.CareRecipient, password));
                                    break;
                                case global::Models.User.AccountType.Admin:
                                    _userLogic.AddNewUser(new Admin(userViewModel.FirstName, userViewModel.LastName,
                                        userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                                        userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                                        (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                        global::Models.User.AccountType.Admin, password));
                                    break;
                                default:
                                    _userLogic.AddNewUser(
                                        new Volunteer(userViewModel.FirstName, userViewModel.LastName, userViewModel.Address,
                                            userViewModel.City, userViewModel.PostalCode, userViewModel.EmailAddress,
                                            Convert.ToDateTime(userViewModel.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                                            global::Models.User.AccountType.Volunteer, password));
                                    break;
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Foutieve email ingevoerd";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Er bestaat al een account met deze e-mail";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "De gebruiker is niet aangemaakt";
                return View();
            }


            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult CreateAccountProfessional()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccountProfessional(UserViewModel userViewModel, string password, string passwordValidation)
        {
            try
            {
                if (password == passwordValidation)
                {
                    if (_userLogic.CheckIfUserAlreadyExists(userViewModel.EmailAddress))
                    {
                        if (_userLogic.IsEmailValid(userViewModel.EmailAddress))
                        {
                            _userLogic.AddNewUser(new CareRecipient(userViewModel.FirstName, userViewModel.LastName,
                            userViewModel.Address, userViewModel.City, userViewModel.PostalCode,
                            userViewModel.EmailAddress, Convert.ToDateTime(userViewModel.BirthDate),
                            (User.Gender)Enum.Parse(typeof(User.Gender), userViewModel.UserGender), true,
                            global::Models.User.AccountType.Professional, password));
                        }
                        else
                        {
                            ViewBag.Message = "Foutieve e-mail ingevoerd";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Er bestaat al een account met dit e-mailadres";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "De wachtwoorden komen niet overheen";
                    return View();
                }
            }
            catch (FormatException)
            {
                ViewBag.Message = "De geboortedatum is onjuist ingevoerd";
                return View();
            }


            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult AccountOverview()
        {
            string email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

            User currentUser = _userLogic.GetCurrentUserInfo(email);
            return View("AccountOverview", new UserViewModel(currentUser));
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditAccount()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid)?.Value);

            User user = _userLogic.GetUserById(userId);

            return View("EditAccount", new UserViewModel(user));
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditAccount(UserViewModel userView)
        {
            try
            {
                if (!_userLogic.CheckIfUserAlreadyExists(userView.EmailAddress))
                {
                    if (_userLogic.IsEmailValid(userView.EmailAddress))
                    {


                        switch (userView.UserAccountType)
                        {
                            case global::Models.User.AccountType.CareRecipient:
                                _userLogic.EditUser(new CareRecipient(userView.UserId, userView.FirstName, userView.LastName,
                                    userView.Address, userView.City, userView.PostalCode,
                                    userView.EmailAddress, Convert.ToDateTime(userView.BirthDate),
                                    (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                                    global::Models.User.AccountType.CareRecipient, ""), "");
                                break;
                            case global::Models.User.AccountType.Admin:
                                _userLogic.EditUser(new Admin(userView.UserId, userView.FirstName, userView.LastName,
                                    userView.Address, userView.City, userView.PostalCode,
                                    userView.EmailAddress, Convert.ToDateTime(userView.BirthDate),
                                    (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                                    global::Models.User.AccountType.Admin, ""), "");
                                break;
                            default:
                                _userLogic.EditUser(new Volunteer(userView.UserId, userView.FirstName, userView.LastName, userView.Address,
                                    userView.City, userView.PostalCode, userView.EmailAddress,
                                    Convert.ToDateTime(userView.BirthDate), (User.Gender)Enum.Parse(typeof(User.Gender), userView.UserGender), true,
                                    global::Models.User.AccountType.Volunteer, ""), "");
                                break;
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Foutieve email ingevoerd";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Er bestaat al een account met dit e-mailadres";
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Message = "De gegevens zijn onjuist ingevoerd.";
                return RedirectToAction("EditAccount");
            }

            return RedirectToAction("AccountOverview");
        }

        [Authorize(Policy = "Admin")]
        public ActionResult BlockUser(int userId)
        {
            User updatedUser = _userLogic.GetUserById(userId);

            updatedUser.Status = !updatedUser.Status;

            _userLogic.EditUser(updatedUser, "");

            _logLogic.CreateUserLog(userId, updatedUser);

            return RedirectToAction("Logout");
        }
    }
}