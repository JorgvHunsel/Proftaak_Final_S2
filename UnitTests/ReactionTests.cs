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
    public class ReactionTests
    {
        [TestMethod]
        public void PostReaction_IsValid()
        {
            Mock<IReactionContext> mockContext = new Mock<IReactionContext>();
            Mock<Reaction> mockReaction = new Mock<Reaction>(1, 2, "foo");

            mockContext.Setup(x => x.PostReaction(mockReaction.Object));

            ReactionLogic reactionLogic = new ReactionLogic(mockContext.Object);
            reactionLogic.PostReaction(mockReaction.Object);

            mockContext.Verify(x => x.PostReaction(mockReaction.Object), Times.Exactly(1));
        }

        [TestMethod]
        public void GetAllCommentsWithQuestionId()
        {
            Mock<IReactionContext> mockContext = new Mock<IReactionContext>();
            Mock<Category> category = new Mock<Category>("Medisch");
            Mock<Question> question = new Mock<Question>(1, "foo", "baa", Question.QuestionStatus.Open, DateTime.Today,
                true, category.Object, 12);
            List<Reaction> mockList = new List<Reaction>();
            mockContext.Setup(x => x.GetAllReactions(question.Object.QuestionId))
                .Returns(mockList);

            ReactionLogic reactionLogic = new ReactionLogic(mockContext.Object);
            List<Reaction> result = reactionLogic.GetAllCommentsWithQuestionId(question.Object.QuestionId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Reaction>));
            mockContext.Verify(x => x.GetAllReactions(1), Times.Exactly(1));
        }
    }
}