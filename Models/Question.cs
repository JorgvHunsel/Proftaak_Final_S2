using System;

namespace Models
{
    public class Question
    {
        public enum QuestionStatus { Open, Closed }
        public int QuestionId { get; }
        public string Title { get; }
        public string Content { get; }
        public QuestionStatus Status { get; set; }
        public DateTime Date { get; set; }
        public bool Urgency { get; set; }
        public Category Category { get; }
        public int CareRecipientId { get; }
        public int CategoryId { get; set; }

        public Question(int questionId, string title, string content, QuestionStatus status, DateTime date, bool urgency, Category category, int careRecipientId)
        {
            QuestionId = questionId;
            Title = title;
            Content = content;
            Status = status;
            Date = date;
            Urgency = urgency;
            Category = category;
            CareRecipientId = careRecipientId;
        }

        public Question(string title, string content, QuestionStatus status, bool urgency, int categoryId, int careRecipientId)
        {
            Title = title;
            Content = content;
            Status = status;
            Urgency = urgency;
            CategoryId = categoryId;
            CareRecipientId = careRecipientId;
        }

        public Question(int questionId)
        {
            QuestionId = questionId;
        }

        public override string ToString()
        {
            return $" '{Status}', '{Title}', '{Content}', '{Date:yyyy-M-d hh:mm tt}', '{Urgency}', '{Category.CategoryId}' ";
        }
    }
}
