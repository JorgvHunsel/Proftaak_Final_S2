using System;
using System.ComponentModel.DataAnnotations;
using Models;

namespace ProftaakASP_S2.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Het onderwerp moet worden ingevuld!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "De inhoud moet worden ingevuld!")]
        public string Content { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public bool Urgency { get; set; }
        public string Category { get; set; }
        public int CareRecipientId { get; set; }
        public string CareRecipientName { get; set; }
        public int CategoryId { get; set; }

        public QuestionViewModel(Question question, User volunteer)
        {
            QuestionId = question.QuestionId;
            Title = question.Title;
            Content = question.Content;
            Status = question.Status.ToString();
            Date = question.Date;
            Urgency = question.Urgency;
            Category = question.Category.Name;
            CareRecipientId = question.CareRecipientId;
            CareRecipientName = volunteer.FirstName;
        }

        public QuestionViewModel(Question question)
        {
            QuestionId = question.QuestionId;
            Title = question.Title;
            Content = question.Content;
            Status = question.Status.ToString();
            Date = question.Date;
            Urgency = question.Urgency;
            Category = question.Category.Name;
            CareRecipientId = question.CareRecipientId;
        }

        public QuestionViewModel()
        {
            
        }

        public QuestionViewModel(int questionId, string title, string careRecipientName)
        {
            QuestionId = questionId;
            Title = title;
            CareRecipientName = careRecipientName;
        }
    }
}
