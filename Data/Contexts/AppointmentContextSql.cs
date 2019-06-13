using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class AppointmentContextSql : IAppointmentContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CreateAppointment(Appointment appointment)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("CreateAppointment", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@questionID", appointment.QuestionId);
                    cmd.Parameters.AddWithValue("@careRecipientID", appointment.CareRecipientId);
                    cmd.Parameters.AddWithValue("@volunteerID", appointment.VolunteerId);
                    cmd.Parameters.AddWithValue("@timestampAppointment", appointment.TimeStampAppointment);
                    cmd.Parameters.AddWithValue("@timestampCreation", appointment.TimeStampCreation);
                    cmd.Parameters.AddWithValue("@location", appointment.Location);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Appointment not created");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Appointment> GetAllAppointmentsFromUser(int userId)
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllAppointmentsFromUser", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int appointmentId = Convert.ToInt32(dr["Appointment_ID"]);
                    int questionId = Convert.ToInt32(dr["Question_ID"]);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipient_ID"]);
                    int volunteerId = Convert.ToInt32(dr["Volunteer_ID"]);
                    DateTime timestampCreation = Convert.ToDateTime(dr["TimeStamp_creation"]);
                    DateTime timestampAppointment = Convert.ToDateTime(dr["TimeStamp_appointment"]);
                    string location = dr["Location"].ToString();

                    appointments.Add(new Appointment(appointmentId, questionId, careRecipientId, volunteerId,
                        timestampCreation, timestampAppointment, location));
                }

                return appointments;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteAppointment(int appointmentId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteAppointment", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AppointmentId", SqlDbType.Int).Value = appointmentId;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Appoinment not created");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteAppointmentByChat(int chatId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteAppointmentByChat", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ChatId", SqlDbType.Int).Value = chatId;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Appointment not deleted");
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}