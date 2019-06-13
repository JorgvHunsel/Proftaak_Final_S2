using System;
using System.Collections.Generic;

namespace Models
{
    public class ChatLog
    {
        public int ChatLogId { get; }
        public string QuestionTitle { get; }
        public int CareRecipientId { get; }
        public int VolunteerId { get; }
        public string CareRecipientName { get; }
        public string VolunteerName { get; }
        public DateTime TimeStamp { get; }
        public List<ChatMessage> Messages = new List<ChatMessage>();
        public int QuestionId { get; }
        public bool Status { get; set; }


        public ChatLog(int chatLogId, string questionTitle, int careRecipientId, int volunteerId, string careRecipientName, string volunteerName,DateTime timeStamp, int questionId, bool status)
        {
            ChatLogId = chatLogId;
            QuestionTitle = questionTitle;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            CareRecipientName = careRecipientName;
            VolunteerName = volunteerName;
            TimeStamp = timeStamp;
            QuestionId = questionId;
            Status = status;
        }

        public ChatLog(int chatLogId, int careRecipientId, int volunteerId, DateTime timeStamp, bool status)
        {
            ChatLogId = chatLogId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStamp = timeStamp;
            Status = status;
        }

        public ChatLog(int chatLogId, int careRecipientId, int volunteerId, string careRecipientName, string volunteerName, DateTime timeStamp, bool status)
        {
            ChatLogId = chatLogId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            CareRecipientName = careRecipientName;
            VolunteerName = volunteerName;
            TimeStamp = timeStamp;
            Status = status;
        }

        public ChatLog(int chatLogId)
        {
            ChatLogId = chatLogId;
        }
    }
}
