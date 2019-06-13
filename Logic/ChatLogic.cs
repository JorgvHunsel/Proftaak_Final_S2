using System.Collections.Generic;
using System.Linq;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class ChatLogic
    {
        private readonly IChatContext _chat;

        public ChatLogic(IChatContext chat)
        {
            _chat = chat;
        }

        public List<ChatLog> GetAllChatLogs()
        {
            return _chat.GetAllChatLogs();
        }

        public void DeleteChatLogFromDatabase(ChatLog chatLog)
        {
            _chat.DeleteChatLog(chatLog);
        }

        public void DeleteMessagesFromDatabase(ChatLog chatLog)
        {
            _chat.DeleteMessages(chatLog);
        }

        public List<ChatMessage> LoadMessageListWithChatId(int chatId) =>
            _chat.LoadMessage(chatId);

        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            _chat.SendMessage(chatid, receiverid, senderid, message);
        }

        public List<ChatLog> GetAllOpenChatsWithVolunteerId(int userid) =>
            _chat.GetAllOpenChatsWithVolunteerId(userid);

        public List<ChatLog> GetAllOpenChatsWithCareRecipientId(int userid) => _chat.GetAllOpenChatsWithCareRecipientId(userid);

        public List<ChatLog> GetAllOpenChatsByDate(int userid)
        {
            return _chat.GetAllOpenChatsWithCareRecipientId(userid).OrderByDescending(c => c.TimeStamp).ToList();
        }

        public int CreateNewChatLog(int reactionId, int volunteerId, int careRecipientId) =>
            _chat.CreateNewChatLog(reactionId, volunteerId, careRecipientId);

        public ChatLog GetSingleChatLog(int chatLogId)
        {
            return _chat.GetSingleChatLog(chatLogId);
        }

        public void ChangeChatStatus(ChatLog chatLog)
        {
            _chat.ChangeChatStatus(chatLog);
        }
    }
}
