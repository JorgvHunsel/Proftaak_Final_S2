using System.Collections.Generic;

namespace ProftaakASP_S2.Models
{
    public class MessageViewModel2
    {
        public List<MessageViewModel> Messages { get; set; }
        public string NewMessage { get; set; }
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public int ChatLogId { get; set; }
        public bool Status { get; set; }

        public MessageViewModel2(int receiverId, int senderId, int chatLogId, bool status)
        {
            ReceiverId = receiverId;
            SenderId = senderId;
            ChatLogId = chatLogId;
            Status = status;
        }

        public MessageViewModel2()
        {

        }
    }
}
