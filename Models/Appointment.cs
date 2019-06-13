using System;

namespace Models
{
    public class Appointment
    {
        public int AppointmentId { get; }
        public int QuestionId { get; }
        public int CareRecipientId { get; }
        public int VolunteerId { get; }
        public DateTime TimeStampCreation { get; }
        public DateTime TimeStampAppointment { get; }
        public string Location { get; }

        public Appointment(int appointmentId, int questionId, int careRecipientId, int volunteerId, DateTime timeStampCreation, DateTime timeStampAppointment, string location)
        {
            AppointmentId = appointmentId;
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStampCreation = timeStampCreation;
            TimeStampAppointment = timeStampAppointment;
            Location = location;
        }

        public Appointment(int questionId, int careRecipientId, int volunteerId, DateTime timeStampCreation, DateTime timeStampAppointment, string location)
        {
            QuestionId = questionId;
            CareRecipientId = careRecipientId;
            VolunteerId = volunteerId;
            TimeStampCreation = timeStampCreation;
            TimeStampAppointment = timeStampAppointment;
            Location = location;
        }
    }
}
