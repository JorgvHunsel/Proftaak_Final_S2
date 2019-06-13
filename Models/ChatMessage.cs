using System;

namespace Models
{
    public class ChatMessage
    {
        public int ChatId { get; }
        public int ReceiverId { get; }
        public int SenderId { get; }
        public string MessageContent { get; }
        public DateTime TimeStamp { get; }


        public ChatMessage(int chatId, int receiverId, int senderId, string messageContent, DateTime timeStamp)
        {
            ChatId = chatId;
            ReceiverId = receiverId;
            SenderId = senderId;
            MessageContent = messageContent;
            TimeStamp = timeStamp;
        }
    }
}
