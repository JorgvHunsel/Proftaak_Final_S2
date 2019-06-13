using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Data.Interfaces;
using Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Data.Contexts
{
    public class ReviewContextSql : IReviewContext
    {
        private const string ConnectionString =
            @"Data Source=mssql.fhict.local;Initial Catalog=dbi423244;User ID=dbi423244;Password=wsx234;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly SqlConnection Conn = new SqlConnection(ConnectionString);


        public void InsertReview(ReviewInfo review)
        {
            try
            {
                Conn.Open();
                string query =
                    "INSERT INTO [VolunteerReview] (CareRecipientID, VolunteerID, Content, Rating, RatingTime) VALUES (@recipientId, @volunteerId, @content, @rating, @ratingTime)";
                using (SqlCommand insertReview = new SqlCommand(query, Conn))
                {
                    insertReview.Parameters.AddWithValue("@recipientId", review.CareRecipientId);
                    insertReview.Parameters.AddWithValue("@volunteerId", review.VolunteerId);
                    insertReview.Parameters.AddWithValue("@content", review.Review ?? "");
                    insertReview.Parameters.AddWithValue("@rating", review.StarAmount);
                    insertReview.Parameters.AddWithValue("@ratingTime", DateTime.Now);
                    insertReview.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new ArgumentException("Insert review failed.");
            }
            finally
            {
                Conn.Close();
            }
        }

        public List<ReviewInfo> GetAllReviews()
        {
            try
            {
                List<ReviewInfo> reviewList = new List<ReviewInfo>();

                SqlCommand cmd = new SqlCommand("GetAllReviews", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                Conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int reviewId = Convert.ToInt32(dr["Review_ID"]);
                    int volunteerId = Convert.ToInt32(dr["VolunteerID"]);
                    string volunteerFirstName = dr["VFName"].ToString();
                    string volunteerLastName = dr["VLName"].ToString();
                    string careFirstName = dr["CFName"].ToString();
                    string careLastName = dr["CLName"].ToString();
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    string reviewContent = dr["Content"].ToString();
                    int starAmount = Convert.ToInt32(dr["Rating"]);
                    DateTime dateTime = Convert.ToDateTime(dr["RatingTime"]);

                    ReviewInfo review = new ReviewInfo(reviewId, volunteerId, careRecipientId, reviewContent,
                        starAmount, volunteerFirstName, volunteerLastName, careFirstName, careLastName);
                    reviewList.Add(review);
                }

                return reviewList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conn.Close();
                
            }
        }

        public List<ReviewInfo> GetAllReviewsWithVolunteerId(int volunteerId)
        {
            List<ReviewInfo> reviewList = new List<ReviewInfo>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllReviewsByVolunteerId", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                Conn.Open();
                cmd.Parameters.Add("@VolunteerId", SqlDbType.Int).Value = volunteerId;

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int reviewId = Convert.ToInt32(row["Review_ID"]);
                    string volunteerFirstName = row["VFName"].ToString();
                    string volunteerLastName = row["VLName"].ToString();
                    string careFirstName = row["CFName"].ToString();
                    string careLastName = row["CLName"].ToString();
                    int careRecipientId = Convert.ToInt32(row["CareRecipientID"]);
                    string reviewContent = row["Content"].ToString();
                    int starAmount = Convert.ToInt32(row["Rating"]);
                    DateTime dateTime = Convert.ToDateTime(row["RatingTime"]);

                    ReviewInfo review = new ReviewInfo(reviewId, volunteerId, careRecipientId, reviewContent,
                        starAmount, volunteerFirstName, volunteerLastName, careFirstName, careLastName);
                    reviewList.Add(review);
                }

                return reviewList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conn.Close();
            }

        }

        public void DeleteReview(int reviewId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteReview", Conn);
                cmd.Parameters.Add("@reviewId", SqlDbType.Int).Value = reviewId;
                cmd.CommandType = CommandType.StoredProcedure;
                Conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Review not deleted");
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}