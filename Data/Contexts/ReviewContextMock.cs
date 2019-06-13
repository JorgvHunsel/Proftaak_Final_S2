using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class ReviewContextMock : IReviewContext
    {
        public List<ReviewInfo> ReviewList = new List<ReviewInfo>();
        public void InsertReview(ReviewInfo review)
        {
            ReviewList.Add(review);
        }

        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
            return ReviewList;
        }

        public List<ReviewInfo> GetAllReviews()
        {
            return new List<ReviewInfo>();
        }

        public void DeleteReview(int reviewId)
        {
            throw new NotImplementedException();
        }
    }
}
