using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class ChatContextSql : IChatContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public List<ChatLog> GetAllOpenChatsWithVolunteerId(int userid)
        {
            List<ChatLog> chatLogList = new List<ChatLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetChatsByVolunteerID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatLogId = Convert.ToInt32(dr["ChatLogID"]);
                    string questionTitle = dr["Title"].ToString();
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    int volunteerId = Convert.ToInt32(dr["VolunteerID"]);
                    string careRecipientName = dr["CareRecipientFirstName"].ToString() + dr["CareRecipientLastName"].ToString();
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    bool status = Convert.ToBoolean(dr["Status"]);

                    string volunteerName = dr["VolunteerFirstName"].ToString() + dr["VolunteerLastName"].ToString();


                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());

                    ChatLog chatLog = new ChatLog(chatLogId, questionTitle, careRecipientId, volunteerId, careRecipientName, volunteerName, timeStamp, questionId, status);
                    chatLogList.Add(chatLog);
                }

                return chatLogList;
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

        public List<ChatLog> GetAllChatLogs()
        {
            List<ChatLog> chatLogList = new List<ChatLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllChatLogs", _conn);
                cmd.CommandType = CommandType.StoredProcedure;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatLogId = Convert.ToInt32(dr["ChatLogID"]);
                    string questionTitle = dr["Title"].ToString();
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    int volunteerId = Convert.ToInt32(dr["VolunteerID"]);
                    string careRecipientName = dr["CareRecipientFirstName"].ToString() + dr["CareRecipientLastName"].ToString();
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string statusstring = dr["Status"].ToString();

                    bool status = statusstring != "False";

                    string volunteerName = dr["VolunteerFirstName"].ToString() + dr["VolunteerLastName"].ToString();


                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());

                    ChatLog chatLog = new ChatLog(chatLogId, questionTitle, careRecipientId, volunteerId, careRecipientName, volunteerName, timeStamp, questionId, status);
                    chatLogList.Add(chatLog);
                }
                return chatLogList;
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

        public List<ChatLog> GetAllOpenChatsWithCareRecipientId(int userid)
        {
            List<ChatLog> chatLogList = new List<ChatLog>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllChatsByCareRecipientID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatLogId = Convert.ToInt32(dr["ChatLogID"]);
                    string questionTitle = (dr["Title"].ToString());
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    int volunteerId = Convert.ToInt32(dr["VolunteerID"]);
                    string careRecipientName = dr["CareRecipientFirstName"].ToString() + dr["CareRecipientLastName"].ToString();
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string statusstring = dr["Status"].ToString();

                    bool status = statusstring != "False";

                    string volunteerName = dr["VolunteerFirstName"].ToString() + dr["VolunteerLastName"].ToString();


                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());


                    ChatLog chatLog = new ChatLog(chatLogId, questionTitle, careRecipientId, volunteerId, careRecipientName, volunteerName, timeStamp, questionId, status);
                    chatLogList.Add(chatLog);
                }

                return chatLogList;
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

        public void DeleteChatLog(ChatLog chatLog)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteChatLog", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatLogID", SqlDbType.Int).Value = chatLog.ChatLogId;


                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Chatlog not created");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteMessages(ChatLog chatLog)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteMessages", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatLogID", SqlDbType.Int).Value = chatLog.ChatLogId;


                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Delete messages failed");
            }
            finally
            {
                _conn.Close();
            }
        }
        public List<ChatMessage> LoadMessage(int chatId)
        {
            List<ChatMessage> chatMessageList = new List<ChatMessage>();
            try
            {
                SqlCommand cmd = new SqlCommand("LoadMessagesByChatLogID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatid", SqlDbType.Int).Value = chatId;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int chatlogId = Convert.ToInt32(dr["ChatID"].ToString());
                    string content = (dr["Content"].ToString());

                    int senderId = Convert.ToInt32(dr["SenderID"].ToString());
                    int receiverId = Convert.ToInt32(dr["ReceiverID"].ToString());

                    DateTime timeStamp = Convert.ToDateTime(dr["TimeStamp"].ToString());

                    ChatMessage chatMessage = new ChatMessage(chatlogId, receiverId, senderId, content, timeStamp);
                    chatMessageList.Add(chatMessage);
                }

                return chatMessageList;

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

        public void SendMessage(int chatid, int receiverid, int senderid, string message)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertMessage", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatid", SqlDbType.Int).Value = chatid;
                cmd.Parameters.Add("@senderid", SqlDbType.Int).Value = senderid;
                cmd.Parameters.Add("@receiverid", SqlDbType.Int).Value = receiverid;
                cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = message;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Message not send");
            }
            finally
            {
                _conn.Close();
            }
        }

        public int CreateNewChatLog(int reactionId, int volunteerId, int careRecipientId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CreateNewChatLog", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@reactionID", SqlDbType.Int).Value = reactionId;
                cmd.Parameters.Add("@volunteerID", SqlDbType.Int).Value = volunteerId;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = careRecipientId;
                cmd.Parameters.Add("@status", SqlDbType.Bit).Value = 1;

                SqlParameter sqlP = new SqlParameter("@identity", SqlDbType.Int);
                sqlP.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(sqlP);

                _conn.Open();
                cmd.ExecuteNonQuery();
                int id = (int)sqlP.Value;
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ChatLog GetSingleChatLog(int chatLogId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetChatLogById", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@chatLogId", SqlDbType.Int).Value = chatLogId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                int volunteerId = Convert.ToInt32(dt.Rows[0]["UserIdVolunteer"]);
                string volName = dt.Rows[0]["FirstNameVolunteer"].ToString() + " " + dt.Rows[0]["LastNameVolunteer"].ToString();
                
                int careRecipientId = Convert.ToInt32(dt.Rows[0]["UserIdCare"]);
                string careName = dt.Rows[0]["FirstNameCare"].ToString() + " " + dt.Rows[0]["LastNameCare"].ToString();

                DateTime timeStamp = Convert.ToDateTime(dt.Rows[0]["TimeStamp"]);
                string statusString = dt.Rows[0]["Status"].ToString();
                

                bool status = statusString != "False";

                return new ChatLog(chatLogId, careRecipientId, volunteerId, careName, volName, timeStamp, status);

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

        public void ChangeChatStatus(ChatLog chatLog)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("ChatLogChangeStatus", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ChatLogId", SqlDbType.Int).Value = chatLog.ChatLogId;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = chatLog.Status;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Status not changed");
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}
