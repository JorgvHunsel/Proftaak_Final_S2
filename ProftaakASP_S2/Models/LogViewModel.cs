using System;
using Models;

namespace ProftaakASP_S2.Models
{
    public class LogViewModel
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }

        public LogViewModel(Log log, string userName)
        {
            LogId = log.LogId;
            UserId = log.UserId;
            Title = log.Title;
            Description = log.Description;
            DateTime = log.DateTime;
            Username = userName;
        }

        public LogViewModel()
        {
            
        }
    }
}
