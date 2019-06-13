using System.Collections.Generic;
using Models;

namespace Data.Interfaces
{
    public interface IAppointmentContext
    {
        void CreateAppointment(Appointment appointment);
        List<Appointment> GetAllAppointmentsFromUser(int userId);
        void DeleteAppointment(int appointmentId);
        void DeleteAppointmentByChat(int chatId);
    }
}
