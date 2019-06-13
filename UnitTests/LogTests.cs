using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Data.Contexts;
using Logic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class LogTests
    {
        private LogLogic _logLogic;
        private LogContextMock _logContext;

        [TestInitialize]
        public void TestInitializer()
        {
            _logContext = new LogContextMock();
            _logLogic = new LogLogic(_logContext);
        }

        [TestMethod]
        public void CreateUserLog()
        {
            Assert.AreEqual(0, _logContext.LogList.Count);

            User user = new Volunteer(14, "Wesley", "Martens", "Drenthelaan 1", "Drenthe", "2101SZ",
                "wesley@hotmail.com", DateTime.Now, User.Gender.Man, true, User.AccountType.Professional, "1111");
            _logLogic.CreateUserLog(user.UserId, user);
            Assert.AreEqual(14, _logContext.LogList[0].UserId);

            User user2 = new Volunteer(15, "Boaz", "Martens", "Drenthelaan 1", "Drenthe", "2101SZ", "boaz@hotmail.com",
                DateTime.Now, User.Gender.Man, false, User.AccountType.Professional, "1111");
            _logLogic.CreateUserLog(user2.UserId, user2);
            Assert.AreEqual(15, _logContext.LogList[1].UserId);
        }

        [TestMethod]
        public void GetAllLogs()
        {
            Assert.AreEqual(0, _logLogic.GetAllLogs().Count);

            _logContext.LogList.Add(new Log(1, 1, "title", "description", DateTime.Now));
            Assert.AreEqual(1, _logLogic.GetAllLogs().Count);

            _logContext.LogList.Add(new Log(2, 1, "title", "description", DateTime.Now));
            Assert.AreEqual(2, _logLogic.GetAllLogs().Count);
        }
    }
}