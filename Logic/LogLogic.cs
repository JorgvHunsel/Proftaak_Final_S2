using System;
using System.Collections.Generic;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class LogLogic
    {
        private readonly ILogContext _logContext;

        public LogLogic(ILogContext logContext)
        {
            _logContext = logContext;
        }

        public void CreateUserLog(int userId, User user)
        {
            string title = user.Status ? "Unblocked user" : "Blocked user";

            Log log = new Log(userId, title, $"User: {user.UserId}, {user.FirstName}", DateTime.Now);

            _logContext.CreateUserLog(log);
        }

        public List<Log> GetAllLogs()
        {
            return _logContext.GetAllLogs();
        }
    }
}
