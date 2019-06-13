using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ReviewInfo
    {
        public int ReviewId { get; set; }
        public int VolunteerId { get; set; }
        public int CareRecipientId { get; set; }
        public string Review { get; set; }
        public int StarAmount { get; set; }
        public string VolFirstName { get; set; }
        public string VolLastName { get; set; }
        public string CareFirstName { get; set; }
        public string CareLastName { get; set; }
        public int QuestionId { get; set; }

        public ReviewInfo(int reviewId, int volunteerId, int careRecipientId, string review, int starAmount, string volFirstName, string volLastName, string careFirstName, string careLastName)
        {
            ReviewId = reviewId;
            VolunteerId = volunteerId;
            CareRecipientId = careRecipientId;
            Review = review;
            StarAmount = starAmount;
            VolFirstName = volFirstName;
            VolLastName = volLastName;
            CareFirstName = careFirstName;
            CareLastName = careLastName;
        }

        public ReviewInfo(int reviewId, int volunteerId, int careRecipientId, string review, int starAmount)
        {
            ReviewId = reviewId;
            VolunteerId = volunteerId;
            CareRecipientId = careRecipientId;
            Review = review;
            StarAmount = starAmount;
        }

        public ReviewInfo(int volunteerId, int careRecipientId, string review, int starAmount)
        {
            VolunteerId = volunteerId;
            CareRecipientId = careRecipientId;
            Review = review;
            StarAmount = starAmount;
        }
    }
}