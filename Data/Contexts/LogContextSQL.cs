using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data.Interfaces;
using Models;

namespace Data.Contexts
{
    public class LogContextSql : ILogContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void CreateUserLog(Log log)
        {
            try
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("AddUserLog", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", log.UserId);
                    cmd.Parameters.AddWithValue("@Title", log.Title);
                    cmd.Parameters.AddWithValue("@Description", log.Description);
                    cmd.Parameters.AddWithValue("@Datetime", log.DateTime);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
               throw new ArgumentException("Userlog not created");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Log> GetAllLogs()
        {
            List<Log> logList = new List<Log>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllLogs", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int logId = Convert.ToInt32(row["LogID"]);
                    int userId = Convert.ToInt32(row["User_ID"]);
                    string title = row["Title"].ToString();
                    string description = row["Description"].ToString();
                    DateTime dateTime = Convert.ToDateTime(row["Datetime"]);

                    logList.Add(new Log(logId, userId, title, description, dateTime));
                }

                return logList;
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
    }
}
