using System;
using System.Collections.Generic;
using System.Text;
using Data.Contexts;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class ReviewTests
    {
        private ReviewLogic _reviewLogic;
        private ReviewContextMock _reviewContext;

        [TestInitialize]
        public void TestInitializer()
        {
            _reviewContext = new ReviewContextMock();
            _reviewLogic = new ReviewLogic(_reviewContext);
        }

        [TestMethod]
        public void InsertReview()
        {
            Assert.AreEqual(0, _reviewLogic.GetAllReviewsWithVolunteerId(1).Count);

            ReviewInfo reviewInfo = new ReviewInfo(1, 1, 1, "test", 5);
            _reviewLogic.InsertReview(reviewInfo);
            Assert.AreEqual(1, _reviewLogic.GetAllReviewsWithVolunteerId(1).Count);
        }
    }
}
