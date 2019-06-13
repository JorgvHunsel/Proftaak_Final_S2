using System;
using Models;

namespace ProftaakASP_S2.Models
{
    public class ReactionViewModel
    {
        public int ReactionId { get; set; }
        public int QuestionId { get; set; }
        public int SenderId { get; set; }
        public string Description { get; set; }
        public string VolunteerName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string QuestionTitle { get; set; }
        public string CareRecipientName { get; set; }

        public ReactionViewModel()
        {
            
        }

        public ReactionViewModel(int reactionId, int questionId, int senderId, string description, string volunteerName, DateTime timeStamp)
        {
            ReactionId = reactionId;
            QuestionId = questionId;
            SenderId = senderId;
            Description = description;
            VolunteerName = volunteerName;
            TimeStamp = timeStamp;
        }

        public ReactionViewModel(Reaction reaction, Question question, User user)
        {
            ReactionId = reaction.ReactionId;
            QuestionId = reaction.QuestionId;
            SenderId = reaction.SenderId;
            Description = reaction.Description;
            VolunteerName = reaction.VolunteerName;
            TimeStamp = reaction.TimeStamp;
            QuestionTitle = question.Title;
            CareRecipientName = user.FirstName;
        }

        public ReactionViewModel(int questionId, string question, string careRecipientName)
        {
            QuestionId = questionId;
            QuestionTitle = question;
            CareRecipientName = careRecipientName;
        }
    }
}
