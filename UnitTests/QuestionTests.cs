using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Data.Interfaces;
using Models;
using System;
using System.Linq;
using Logic;

namespace UnitTests
{
    [TestClass]
    public class QuestionTests
    {
        private const string MockMaxCharacters =
            "athornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNmreynault01PGnJuDB9uNNathornthwaite0mreynault1PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNathornthwaite0mreynault01PGnJuDB9uNNatho@thetimes.co.uk";

        [TestMethod]
        public void WriteQuestionToDatabase_IsValid_True()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> category = new Mock<Category>("Medisch");
            Mock<Question> question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            mockContext.Setup(x => x.AddQuestion(question.Object));

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            questionLogic.WriteQuestionToDatabase(question.Object);
            mockContext.Verify(x => x.AddQuestion(question.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void WriteQuestionToDatabase_IsValid_False()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();

            Mock<Category> category = new Mock<Category>("Medisch");

            //To much characters
            Mock<Question> question = new Mock<Question>(1, MockMaxCharacters, "baa", Question.QuestionStatus.Open,
                DateTime.Today, true, category.Object, 12);
            Mock<Question> question2 = new Mock<Question>(1, "sup", MockMaxCharacters, Question.QuestionStatus.Open,
                DateTime.Today, true, category.Object, 12);

            //Empty properties
            Mock<Question> question3 = new Mock<Question>(1, "", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            Mock<Question> question4 = new Mock<Question>(1, "sup", "", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);

            Assert.ThrowsException<ArgumentException>(() => questionLogic.WriteQuestionToDatabase(question.Object));
            Assert.ThrowsException<ArgumentException>(() => questionLogic.WriteQuestionToDatabase(question2.Object));

            Assert.ThrowsException<ArgumentException>(() => questionLogic.WriteQuestionToDatabase(question3.Object));
            Assert.ThrowsException<ArgumentException>(() => questionLogic.WriteQuestionToDatabase(question4.Object));
        }

        [TestMethod]
        public void GetAllOpenQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            mockContext.Setup(x => x.GetAllOpenQuestionsVolunteer())
                .Returns(stub);

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            List<Question> result = questionLogic.GetAllOpenQuestions();

            Assert.IsInstanceOfType(result, typeof(List<Question>));
        }

        [TestMethod]
        public void GetAllOpenQuestionsCareRecipientID_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
                "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
                "1111").Object;
            mockContext.Setup(x => x.GetAllOpenQuestionsCareRecipient(user.UserId))
                .Returns(stub);

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            List<Question> result = questionLogic.GetAllOpenQuestionCareRecipientId(user.UserId);

            Assert.IsInstanceOfType(result, typeof(List<Question>));
        }

        [TestMethod]
        public void GetAllClosedQuestionsCareRecipientID_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            List<Question> stub = new List<Question>();
            User user = new Mock<User>(1, "Jesse", "Oosterwijk", "Kleidonk 1", "Beuningen", "6641LM",
                "jesse.oosterwijk@outlook.com", DateTime.Today, User.Gender.Man, true, User.AccountType.CareRecipient,
                "1111").Object;
            mockContext.Setup(x => x.GetAllClosedQuestionsCareRecipient(user.UserId))
                .Returns(stub);

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            List<Question> result = questionLogic.GetAllClosedQuestionsCareRecipientId(user.UserId);

            Assert.IsInstanceOfType(result, typeof(List<Question>));
        }


        [TestMethod]
        public void GetSingleQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> category = new Mock<Category>("Medisch");
            Mock<Question> question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            mockContext.Setup(x => x.GetSingleQuestion(question.Object.QuestionId))
                .Returns(question.Object);

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            Question result = questionLogic.GetSingleQuestion(question.Object.QuestionId);

            Assert.IsInstanceOfType(result, typeof(Question));
        }

        [TestMethod]
        public void EditQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> category = new Mock<Category>("Medisch");
            Mock<Question> question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            mockContext.Setup(x => x.EditQuestion(question.Object));

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            questionLogic.EditQuestion(question.Object);

            mockContext.Verify(x => x.EditQuestion(question.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void DeleteQuestion_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();
            Mock<Category> category = new Mock<Category>("Medisch");
            Mock<Question> question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            mockContext.Setup(x => x.DeleteQuestion(question.Object));

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            questionLogic.DeleteQuestionFromDatabase(question.Object);

            mockContext.Verify(x => x.DeleteQuestion(question.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllQuestions_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();

            QuestionLogic questionLogic = new QuestionLogic(mockContext.Object);
            questionLogic.GetAllQuestions();

            mockContext.Verify(x => x.GetAllQuestions(), Times.Exactly(1));
        }

        [TestMethod]
        public void ChangeQuestionStatus_IsValid()
        {
            Mock<IQuestionContext> mockContext = new Mock<IQuestionContext>();

            Category category = new Category("Medisch");
            Question question = new Question(1, "foo", "baa", Question.QuestionStatus.Closed, DateTime.Today, true,
                category, 12);

            QuestionLogic logic = new QuestionLogic(mockContext.Object);

            logic.ChangeStatus(question.QuestionId, Question.QuestionStatus.Closed.ToString());

            Assert.AreNotEqual(question.Status.ToString(), "Open");
        }
    }
}