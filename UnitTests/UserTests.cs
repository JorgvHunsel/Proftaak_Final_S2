using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using Models;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class UserTests
    {
        User _user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
            "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
            "1111").Object;

        Mock<IUserContext> _mockContext = new Mock<IUserContext>();

        [TestMethod]
        public void AddNewUser_IsValid()
        {
            _mockContext.Setup(x => x.AddNewUser(_user));

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            userLogic.AddNewUser(_user);

            _mockContext.Verify(x => x.AddNewUser(_user), Times.Exactly(1));
        }

        [TestMethod]
        public void EditUser_IsValid()
        {
            _mockContext.Setup(x => x.EditUser(_user, "secret"));

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            userLogic.EditUser(_user, "secret");

            _mockContext.Verify(x => x.EditUser(_user, "secret"), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllUsers_IsValid()
        {
            List<User> testList = new List<User>();

            _mockContext.Setup(x => x.GetAllUsers())
                .Returns(testList);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            List<User> result = userLogic.GetAllUsers();

            _mockContext.Verify(x => x.GetAllUsers(), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(List<User>));
        }

        [TestMethod]
        public void GetUserId_IsValid()
        {
            _mockContext.Setup(x => x.GetUserId(_user.EmailAddress))
                .Returns(It.IsAny<int>);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            int result = userLogic.GetUserId(_user.EmailAddress);

            _mockContext.Verify(x => x.GetUserId(_user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void CheckIfAccountIsActive_IsValid()
        {
            _mockContext.Setup(x => x.CheckIfAccountIsActive(_user.EmailAddress))
                .Returns(It.IsAny<bool>);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            bool result = userLogic.CheckIfAccountIsActive(_user.EmailAddress);

            _mockContext.Verify(x => x.CheckIfAccountIsActive(_user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void CheckValidityUser_IsValid_True()
        {
            _mockContext.Setup(x => x.CheckValidityUser(_user.EmailAddress, "secret"))
                .Returns(_user);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            User result = userLogic.CheckValidityUser(_user.EmailAddress, "secret");

            _mockContext.Verify(x => x.CheckValidityUser(_user.EmailAddress, "secret"), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void CheckValidityUser_IsValid_False()
        {
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
                "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
                "1111").Object;

            UserLogic userLogic = new UserLogic(_mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() => userLogic.CheckValidityUser("", user.Password));
            Assert.ThrowsException<ArgumentException>(() => userLogic.CheckValidityUser(user.EmailAddress, ""));

            Assert.ThrowsException<ArgumentException>(() =>
                userLogic.CheckValidityUser("athornthwaite0mreynault01PGnJuDB9uNN@thetimes.co.uk", "1111"));
            Assert.ThrowsException<ArgumentException>(() => userLogic.CheckValidityUser("Wesley.Martens@hotmail.com",
                "173xdEamUX9D9nCeQrJ6e9HkBLQE3DwZtU14RW6PegHKonJ4gwS"));
        }

        [TestMethod]
        public void CheckIfUserAlreadyExists_IsValid()
        {
            _mockContext.Setup(x => x.CheckIfUserAlreadyExists(_user.EmailAddress))
                .Returns(It.IsAny<bool>);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            bool result = userLogic.CheckIfUserAlreadyExists(_user.EmailAddress);

            _mockContext.Verify(x => x.CheckIfUserAlreadyExists(_user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void GetCurrentUserInfo_IsValid()
        {
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
                "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
                "1111").Object;

            _mockContext.Setup(x => x.GetUserInfo(user.EmailAddress))
                .Returns(user);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            User result = userLogic.GetCurrentUserInfo(user.EmailAddress);

            _mockContext.Verify(x => x.GetUserInfo(user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void GetUserById_IsValid()
        {
            _mockContext.Setup(x => x.GetUserById(_user.UserId))
                .Returns(_user);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            User result = userLogic.GetUserById(_user.UserId);

            _mockContext.Verify(x => x.GetUserById(_user.UserId), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(User));
        }

        [TestMethod]
        public void IsEmailValid_IsValid()
        {
            _mockContext.Setup(x => x.IsEmailValid(_user.EmailAddress))
                .Returns(It.IsAny<bool>);

            UserLogic userLogic = new UserLogic(_mockContext.Object);
            bool result = userLogic.IsEmailValid(_user.EmailAddress);

            _mockContext.Verify(x => x.IsEmailValid(_user.EmailAddress), Times.Exactly(1));
            Assert.IsInstanceOfType(result, typeof(bool));
        }
    }
}