using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProftaakASP_S2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProftaakASP_S2.Controllers
{
    [Authorize(Policy = "CareRecipient")]
    public class CareRecipientController : Controller
    {
        private readonly QuestionLogic _questionLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ReactionLogic _reactionLogic;
        private readonly UserLogic _userLogic;
        private readonly ChatLogic _chatLogic;
        private readonly AppointmentLogic _appointmentLogic;


        public CareRecipientController(QuestionLogic questionLogic, CategoryLogic categoryLogic, ReactionLogic reactionLogic, UserLogic userLogic, ChatLogic chatLogic, AppointmentLogic appointmentLogic)
        {
            _questionLogic = questionLogic;
            _categoryLogic = categoryLogic;
            _reactionLogic = reactionLogic;
            _userLogic = userLogic;
            _chatLogic = chatLogic;
            _appointmentLogic = appointmentLogic;
        }

        public ActionResult Overview()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            ViewBag.Message = TempData["ErrorMessage"] as string;

            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllOpenQuestionCareRecipientId(userId))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return View("../CareRecipient/Question/Overview", questionView);
        }

        public ActionResult OverviewClosed()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
            ViewBag.Message = TempData["ErrorMessage"] as string;

            List<QuestionViewModel> questionView = new List<QuestionViewModel>();
            foreach (Question question in _questionLogic.GetAllClosedQuestionsCareRecipientId(userId))
            {
                questionView.Add(new QuestionViewModel(question));
            }

            return View("../CareRecipient/Question/OverviewClosed", questionView);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();
            return View("../CareRecipient/Question/Edit", new QuestionViewModel(_questionLogic.GetSingleQuestion(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QuestionViewModel question)
        {
            try
            {
                _questionLogic.EditQuestion(new Question(id, question.Title, question.Content, Question.QuestionStatus.Open, question.Date, question.Urgency, new Category(question.CategoryId), question.CareRecipientId));

                return RedirectToAction("Overview");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        [HttpGet]
        public ActionResult ReactionOverview(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            List<ReactionViewModel> reactionViews = new List<ReactionViewModel>();

            if (_reactionLogic.GetAllCommentsWithQuestionId(id).Count > 0)
            {

                foreach (Reaction reaction in _reactionLogic.GetAllCommentsWithQuestionId(id))
                {
                    reactionViews.Add(new ReactionViewModel(reaction, _questionLogic.GetSingleQuestion(reaction.QuestionId),
                        _userLogic.GetUserById(userId)));
                }

                ViewBag.Message = null;

                return View("Reaction/Overview", reactionViews);
            }

            TempData["ErrorMessage"] = "Vraag heeft geen reacties";
            return RedirectToAction("Overview");
        }

        [HttpGet]
        public ActionResult ReactionOverviewClosed(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            List<ReactionViewModel> reactionViews = new List<ReactionViewModel>();

            if (_reactionLogic.GetAllCommentsWithQuestionId(id).Count > 0)
            {

                foreach (Reaction reaction in _reactionLogic.GetAllCommentsWithQuestionId(id))
                {
                    reactionViews.Add(new ReactionViewModel(reaction, _questionLogic.GetSingleQuestion(reaction.QuestionId),
                        _userLogic.GetUserById(userId)));
                }

                ViewBag.Message = null;

                return View("Reaction/Overview", reactionViews);
            }

            TempData["ErrorMessage"] = "Vraag heeft geen reacties";
            return RedirectToAction("OverviewClosed");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReactionOverview(int id, ReactionViewModel reaction)
        {
            try
            {
                return RedirectToAction("ReactionOverview");
            }
            catch
            {
                return RedirectToAction("Edit");
            }
        }

        public ActionResult Create()
        {
            ViewData["Categories"] = _categoryLogic.GetAllCategories();

            return View("../CareRecipient/Question/Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel question)
        {
            try
            {
                int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);
                _questionLogic.WriteQuestionToDatabase(new Question(question.Title, question.Content, Question.QuestionStatus.Open, question.Urgency, question.CategoryId, userId));

                return RedirectToAction(nameof(Overview));
            }
            catch (Exception)
            {
                return View("../Shared/Error");
            }
        }

        public ActionResult ChangeStatus(int id, string status, string path)
        {
            string[] redirectUrl = path.Split("/");

            _questionLogic.ChangeStatus(id, status);
            if (redirectUrl[2] == "Overview")
            {
                return RedirectToAction(nameof(Overview));
            }

            return RedirectToAction(nameof(OverviewClosed));
        }

        public ActionResult CreateChat(int reactionId, int volunteerId)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            int id = _chatLogic.CreateNewChatLog(reactionId, volunteerId, userId);
            if (id != 0)
            {
                return RedirectToAction("OpenChat", new { id });
            }

            return RedirectToAction(nameof(Overview));
        }

        public ActionResult ChatOverview()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            List<ChatViewModel> chatView = new List<ChatViewModel>();
            foreach (ChatLog chatLog in _chatLogic.GetAllOpenChatsByDate(userId))
            {
                chatView.Add(new ChatViewModel(chatLog));
            }

            return View("../CareRecipient/Chat/Overview", chatView);
        }
        

        public ActionResult OpenChat(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid).Value);

            List<MessageViewModel> messageView = new List<MessageViewModel>();

            ChatLog currentChatLog = _chatLogic.GetSingleChatLog(id);
            string volunteerName = currentChatLog.VolunteerName;
            string careRecipientName = currentChatLog.CareRecipientName;

            MessageViewModel2 messageView2 = new MessageViewModel2(currentChatLog.VolunteerId, userId, id, currentChatLog.Status);

            foreach (ChatMessage cMessage in _chatLogic.LoadMessageListWithChatId(id))
            {
                messageView.Add(new MessageViewModel(cMessage, userId, volunteerName, careRecipientName));
            }

            messageView2.Messages = messageView;
            return View("../CareRecipient/Chat/OpenChat", messageView2);
        }


        public ActionResult NewMessage(MessageViewModel2 mvMessageViewModel2)
        {
            _chatLogic.SendMessage(mvMessageViewModel2.ChatLogId, mvMessageViewModel2.ReceiverId, mvMessageViewModel2.SenderId, mvMessageViewModel2.NewMessage);
            ChatLog currentChatLog = _chatLogic.GetSingleChatLog(mvMessageViewModel2.ChatLogId);
            return RedirectToAction("OpenChat", new { id = currentChatLog.ChatLogId } );
        }


        public ActionResult ChangeStatusChat(int chatlogId)
        {
            ChatLog chatLog = _chatLogic.GetSingleChatLog(chatlogId);

            chatLog.Status = !chatLog.Status;

            _chatLogic.ChangeChatStatus(chatLog);

            _appointmentLogic.DeleteAppointmentByChat(chatlogId);

            return RedirectToAction("ChatOverview");
        }

        [HttpGet]
        public ActionResult RatingOverview(int volunteerId, int questionId)
        {
            ReviewViewModel reviewViewModel = new ReviewViewModel(volunteerId, questionId);

            return View("../Review/Index", reviewViewModel);
        }

        
    }
}