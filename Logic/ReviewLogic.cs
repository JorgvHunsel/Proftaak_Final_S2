using System;
using System.Collections.Generic;
using System.Text;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class ReviewLogic
    {
        private readonly IReviewContext _reviewContext;

        public ReviewLogic(IReviewContext reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public void InsertReview(ReviewInfo review)
        {
            _reviewContext.InsertReview(review);
        }
        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
            return _reviewContext.GetAllReviewsWithVolunteerId(volunteerId);
        }

        public List<ReviewInfo> GetAllReviews()
        {
            return _reviewContext.GetAllReviews();
        }

        public void DeleteReview(int reviewId)
        {
            _reviewContext.DeleteReview(reviewId);
        }
    }
}