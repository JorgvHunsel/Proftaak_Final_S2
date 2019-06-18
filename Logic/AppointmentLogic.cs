using System;
using System.Collections.Generic;
using Data.Interfaces;
using Models;

namespace Logic
{
    public class AppointmentLogic
    {
        private readonly IAppointmentContext _appointment;

        public AppointmentLogic(IAppointmentContext appointment)
        {
            _appointment = appointment;
        }

        public void CreateAppointment(Appointment appointment)
        {
            CheckAppointment(appointment);
            _appointment.CreateAppointment(appointment);
        }

        public void CheckAppointment(Appointment appointment)
        {
            int result = DateTime.Compare(appointment.TimeStampAppointment, DateTime.Now);
            if (result < 0)
            {
                throw new ArgumentException();
            }
        }

        public List<Appointment> GetAllAppointmentsFromUser(int userId)
        {
            return _appointment.GetAllAppointmentsFromUser(userId);
        }

        public void DeleteAppointment(int appointmentId)
        {
            _appointment.DeleteAppointment(appointmentId);
        }

        public void DeleteAppointmentByChat(int chatId)
        {
            _appointment.DeleteAppointmentByChat(chatId);
        }
    }
}
