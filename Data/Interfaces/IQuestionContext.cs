using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IQuestionContext
    {
        void AddQuestion(Question askedQuestion);
        List<Question> GetAllOpenQuestionsVolunteer();
        List<Question> GetAllClosedQuestionsVolunteer(int volunteerId);
        Question GetSingleQuestion(int questionId);
        void EditQuestion(Question question);
        List<Question> GetAllOpenQuestionsCareRecipient(int careRecipientId);
        void ChangeQuestionStatus(int id, string closed);
        List<Question> GetAllClosedQuestionsCareRecipient(int careRecipientId);
        List<Question> GetAllQuestions();
        void DeleteQuestion(Question askedQuestion);
        List<Question> GetAllQuestionsProfessional(int userid, string status);
    }
}
