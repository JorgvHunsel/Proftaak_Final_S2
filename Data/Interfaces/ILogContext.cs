using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface ILogContext
    {
        void CreateUserLog(Log log);
        List<Log> GetAllLogs();
    }
}
