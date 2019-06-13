using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Data.Interfaces
{
    public interface IReviewContext
    {
        void InsertReview(ReviewInfo review);
        List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId);
        List<ReviewInfo> GetAllReviews();
        void DeleteReview(int reviewId);
    }
}