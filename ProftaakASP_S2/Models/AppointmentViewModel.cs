using System;
using Models;

namespace ProftaakASP_S2.Models
{
    public class AppointmentViewModel
    {
        public int QuestionId { get; set; }
        public int AppointmentId { get; set; }
        public int CareRecipientId { get; set; }
        public int VolunteerId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string QuestionTitle { get; set; }
        public string CareRecipientName { get; set; }
        public string Location { get; set; }

        public AppointmentViewModel(Appointment appointment, Question question, User user)
        {
            QuestionId = appointment.QuestionId;
            CareRecipientId = appointment.CareRecipientId;
            VolunteerId = appointment.VolunteerId;
            TimeStamp = appointment.TimeStampAppointment;
            QuestionTitle = question.Title;
            CareRecipientName = user.FirstName;
            Location = appointment.Location;
            AppointmentId = appointment.AppointmentId;
        }

        public AppointmentViewModel(int questionId, int careRecipientId, int volunteerId, string location, int appointmentId)
        {
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            Location = location;
            AppointmentId = appointmentId;
        }

        public AppointmentViewModel()
        {
            
        }
    }
}
