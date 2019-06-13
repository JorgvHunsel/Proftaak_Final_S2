using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IChatContext
    {
        List<ChatLog> GetAllOpenChatsWithVolunteerId(int userid);
        List<ChatLog> GetAllOpenChatsWithCareRecipientId(int userid);
        List<ChatMessage> LoadMessage(int chatId);
        void SendMessage(int chatid, int receiverid, int senderid, string message);
        int CreateNewChatLog(int reactionId, int volunteerId, int careRecipientId);
        void DeleteChatLog(ChatLog chatLog);
        void DeleteMessages(ChatLog chatLog);
        List<ChatLog> GetAllChatLogs();
        ChatLog GetSingleChatLog(int chatLogId);
        void ChangeChatStatus(ChatLog chatLog);
    }
}
