using System;
using Models;

namespace ProftaakASP_S2.Models
{
    public class ChatViewModel
    {
        public int ChatLogId { get; set; }
        public string VolunteerName { get; set; }
        public string CareRecipientName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string QuestionName { get; set; }
        public int QuestionId { get; set; }
        public int VolunteerId { get; set; }
        public int CareRecipientId { get; set; }
        public bool Status { get; set; }

        public ChatViewModel(ChatLog chat)
        {
            ChatLogId = chat.ChatLogId;
            VolunteerName = chat.VolunteerName;
            CareRecipientName = chat.CareRecipientName;
            TimeStamp = chat.TimeStamp;
            QuestionName = chat.QuestionTitle;
            QuestionId = chat.QuestionId;
            VolunteerId = chat.VolunteerId;
            CareRecipientId = chat.CareRecipientId;
            Status = chat.Status;
        }

        public ChatViewModel(int chatlogId)
        {
            ChatLogId = chatlogId;
        }

        public ChatViewModel()
        {
            
        }
    }

}
