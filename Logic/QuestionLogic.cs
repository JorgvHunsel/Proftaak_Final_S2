using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;


namespace Logic
{
    public class QuestionLogic
    {
        private readonly IQuestionContext _question;

        public QuestionLogic(IQuestionContext question)
        {
            _question = question;
        }

        public void WriteQuestionToDatabase(Question askedQuestion)
        {
            if (askedQuestion.Title == "")
                throw new ArgumentException("Title can't be empty");
            if (askedQuestion.Title.Length > 100)
                throw new ArgumentException("Title can't be too long");

            if (askedQuestion.Content == "")
                throw new ArgumentException("Content can't be empty");
            if (askedQuestion.Content.Length > 500)
                throw new ArgumentException("Content can't be too long");

            _question.AddQuestion(askedQuestion);
        }

        public void DeleteQuestionFromDatabase(Question askedQuestion)
        {
            _question.DeleteQuestion(askedQuestion);
        }

        public List<Question> GetAllQuestionsProfessional(int userid, string statusrequest)
        {
            return _question.GetAllQuestionsProfessional(userid, statusrequest);
        }

        public List<Question> GetAllOpenQuestions()
        {
            return _question.GetAllOpenQuestionsVolunteer();
        }

        public List<Question> GetAllClosedQuestionsVolunteer(int volunteerId)
        {
            return _question.GetAllClosedQuestionsVolunteer(volunteerId);
        }

        public List<Question> GetAllOpenQuestionCareRecipientId(int careRecipientId)
        {
            return _question.GetAllOpenQuestionsCareRecipient(careRecipientId);
        }

        public Question GetSingleQuestion(int questionId)
        {
            return _question.GetSingleQuestion(questionId);
        }

        public void EditQuestion(Question question)
        {
            _question.EditQuestion(question);
        }

        public void ChangeStatus(int id, string status)
        {
            _question.ChangeQuestionStatus(id, status == "Open" ? "Closed" : "Open");
        }

        public List<Question> GetAllClosedQuestionsCareRecipientId(int careRecipient)
        {
            return _question.GetAllClosedQuestionsCareRecipient(careRecipient);
        }

        public List<Question> GetAllQuestions()
        {
            return _question.GetAllQuestions();
        }

    }
}
