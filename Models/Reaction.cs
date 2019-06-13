using System;

namespace Models
{
    public class Reaction
    {
       public int ReactionId { get; set; }
       public int QuestionId { get; set; }
       public int SenderId { get; set; }
       public string Description { get; set; }
       public string VolunteerName { get; set; }
       public DateTime TimeStamp { get; set; }

       public Reaction(int questionId, int senderId, string description, string volunteerName)
       {
           QuestionId = questionId;
           SenderId = senderId;
           Description = description;
           VolunteerName = volunteerName;
       }

       public Reaction(int questionId, int senderId, string description)
       {
           QuestionId = questionId;
           SenderId = senderId;
           Description = description;
       }

       public Reaction(int reactionId, int questionId, int senderId, string description, string volunteerName, DateTime timeStamp)
       {
           ReactionId = reactionId;
            QuestionId = questionId;
           SenderId = senderId;
           Description = description;
           VolunteerName = volunteerName;
           TimeStamp = timeStamp;
       }


        public override string ToString()
       {
           return $" '{QuestionId}', '{SenderId}', '{Description}' ";
       }
    }
}
