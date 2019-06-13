using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Microsoft.AspNetCore.Authorization;
using ProftaakASP_S2.Models;
using Models;

namespace ProftaakASP_S2.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {

        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;
        private readonly LogLogic _logLogic;
        private readonly ReviewLogic _reviewLogic;

        public AdminController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic, LogLogic logLogic, ReviewLogic reviewLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
            _logLogic = logLogic;
            _reviewLogic = reviewLogic;
        }

        public ActionResult ChatLogOverview()
        {
            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllChatLogs())
            {
                chatView.Add(new ChatViewModel(chatLog));
            }

            return View("../Admin/ChatLogOverview", chatView);
        }

        public ActionResult ChatLogDelete(ChatViewModel chat)
        {
            try
            {
                _chatLogic.DeleteMessagesFromDatabase(new ChatLog(chat.ChatLogId));
                _chatLogic.DeleteChatLogFromDatabase(new ChatLog(chat.ChatLogId));

                return RedirectToAction("ChatLogOverview");
            }
            catch
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult QuestionOverview()
        {
            List<QuestionViewModel> questionViews = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllQuestions())
            {
                questionViews.Add(new QuestionViewModel(question, _userLogic.GetUserById(question.CareRecipientId)));
            }

            return View("../Admin/QuestionOverview", questionViews);
        }

        public ActionResult QuestionDelete(QuestionViewModel question)
        {
            try
            {
                _questionLogic.DeleteQuestionFromDatabase(new Question(question.QuestionId));

                return RedirectToAction("QuestionOverview");
            }
            catch
            {
                return View("../Shared/Error");
            }
        }


        [HttpGet]
        public ActionResult UserOverview()
        {
            List<UserViewModel> userViewList = new List<UserViewModel>();
            foreach (User user in _userLogic.GetAllUsers())
            {
                userViewList.Add(new UserViewModel(user));
            }

            return View(userViewList);
        }

        public ActionResult BlockUser(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            User updatedUser = _userLogic.GetUserById(id);

            updatedUser.Status = !updatedUser.Status;

            _userLogic.EditUser(updatedUser, "");

            _logLogic.CreateUserLog(userId, updatedUser);

            return RedirectToAction("UserOverview");
        }

        public ActionResult LogOverview()
        {
            List<LogViewModel> logList = new List<LogViewModel>();

            foreach (Log log in _logLogic.GetAllLogs())
            {
                string username = _userLogic.GetUserById(log.UserId).FirstName +
                                  _userLogic.GetUserById(log.UserId).LastName;

                logList.Add(new LogViewModel(log, username));
            }

            return View("LogOverview", logList);
        }

        public ActionResult CreateProfessional()
        {
            return View("CreateProfessional");
        }

        [HttpPost]
        public ActionResult CreateProfessional(string emailaddress)
        {
            _userLogic.SendEmailProfessional(emailaddress);

            return RedirectToAction("UserOverview");
        }



        public ActionResult LinkToProfessional(int userId)
        {
            User careRecipient = _userLogic.GetUserById(userId);
            List<User> professionals = _userLogic.GetAllProfessionals();

            LinkToProfessionalViewModel linkToProfessionalViewModel = new LinkToProfessionalViewModel(careRecipient, professionals);


           return View("LinkCareProfessional", linkToProfessionalViewModel);
        }

        public ActionResult LinkCareToProf(int careId, int profId)
        {
            _userLogic.LinkCareToProf(careId, profId);

            return RedirectToAction("UserOverview");
        }

        public ActionResult ReviewOverview()
        {
            List<ReviewInfo> reviews = _reviewLogic.GetAllReviews();
            List<ReviewViewModel> reviewViewModels = new List<ReviewViewModel>();

            foreach (ReviewInfo review in reviews)
            {
                ReviewViewModel reviewViewModel = new ReviewViewModel(review);
                reviewViewModels.Add(reviewViewModel);
            }

            return View(reviewViewModels);
        }
    }
}