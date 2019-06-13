using System;
using Models;

namespace ProftaakASP_S2.Models
{
    public class MessageViewModel
    {
        public int ChatLogId { get; set; }
        public string MessageContent { get; set; }
        public DateTime Timestamp { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int UserId { get; set; }

        public string VolunteerName { get; set; }
        public string CareRecipientName { get; set; }
        

        public MessageViewModel(ChatMessage cM, int userId, string volunteerName, string careRecipientName)
        {
            UserId = userId;
            ChatLogId = cM.ChatId;
            SenderId = cM.SenderId;
            ReceiverId = cM.ReceiverId;
            MessageContent = cM.MessageContent;
            Timestamp = cM.TimeStamp;
            VolunteerName = volunteerName;
            CareRecipientName = careRecipientName;
        }

        public MessageViewModel(ChatMessage cM, int userId)
        {
            UserId = userId;
            ChatLogId = cM.ChatId;
            SenderId = cM.SenderId;
            ReceiverId = cM.ReceiverId;
            MessageContent = cM.MessageContent;
            Timestamp = cM.TimeStamp;
        }

        public MessageViewModel()
        {
            
        }
    }
}
