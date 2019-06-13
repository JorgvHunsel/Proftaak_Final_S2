using System;

namespace Models
{
    public class Log
    {
        public int LogId { get; }
        public int UserId { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime DateTime { get; }

        public Log(int logId, int userId, string title, string description, DateTime dateTime)
        {
            LogId = logId;
            UserId = userId;
            Title = title;
            Description = description;
            DateTime = dateTime;
        }

        public Log(int userId, string title, string description, DateTime dateTime)
        {
            UserId = userId;
            Title = title;
            Description = description;
            DateTime = dateTime;
        }
    }
}
