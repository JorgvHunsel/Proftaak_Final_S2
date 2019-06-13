using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Logic;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void GetAllOpenChatsWithVolunteerID_IsValid()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IChatContext>()
                    .Setup(x => x.GetAllOpenChatsWithVolunteerId(1))
                    .Returns(GetSampleChats());

                ChatLogic cls = mock.Create<ChatLogic>();
                List<ChatLog> expected = GetSampleChats();

                List<ChatLog> actual = cls.GetAllOpenChatsWithVolunteerId(1);

                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [TestMethod]
        public void GetAllOpenChatsWithCareRecipientID_IsValid()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<IChatContext>()
                    .Setup(x => x.GetAllOpenChatsWithCareRecipientId(1))
                    .Returns(GetSampleChats());

                ChatLogic cls = mock.Create<ChatLogic>();
                List<ChatLog> expected = GetSampleChats();

                List<ChatLog> actual = cls.GetAllOpenChatsWithCareRecipientId(1);

                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [TestMethod]
        public void LoadMessageAsListUsingChatLogID_IsValid()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();
            List<ChatMessage> stub = new List<ChatMessage>();

            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
                "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
                "1111").Object;
            mockContext.Setup(x => x.LoadMessage(user.UserId))
                .Returns(stub);

            ChatLogic chatLogic = new ChatLogic(mockContext.Object);

            List<ChatMessage> result = chatLogic.LoadMessageListWithChatId(user.UserId);
            List<ChatMessage> expected = stub;


            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ChatMessage>));
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void SendMessage_Called_Exactly_Once()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();
            mockContext.Setup(x => x.SendMessage(0, 1, 2, "hoi"));

            ChatLogic chatLogic = new ChatLogic(mockContext.Object);
            chatLogic.SendMessage(0, 1, 2, "hoi");
            mockContext.Verify((x => x.SendMessage(0, 1, 2, "hoi")), Times.Exactly(1));
        }

        [TestMethod]
        public void CreateNewChatLog_IsValid()
        {
            Mock<IChatContext> mockContext = new Mock<IChatContext>();

            int newChatLogId = 3;
            mockContext.Setup(x => x.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(newChatLogId);

            ChatLogic chatLogic = new ChatLogic(mockContext.Object);
            int result = chatLogic.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            mockContext.Verify(x => x.CreateNewChatLog(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(newChatLogId, result);
        }

        private List<ChatLog> GetSampleChats()
        {
            List<ChatLog> output = new List<ChatLog>
            {
                new ChatLog(12, "foo", 12, 13, "doo", "hi", DateTime.Today, 18, true)
            };
            return output;
        }
    }
}