using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class ReactionContextSql : IReactionContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();
        public void PostReaction(Reaction reaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertReaction", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@senderid", SqlDbType.Int).Value = reaction.SenderId;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = reaction.QuestionId;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = reaction.Description;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Reaction not posted");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Reaction> GetAllReactions(int questionId)
        {
            List<Reaction> reactionList = new List<Reaction>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCommentsByQuestionID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@questionID", SqlDbType.Int).Value = questionId;

                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    string volunteerName = dr["VolunteerName"].ToString();
                    string description = dr["Description"].ToString();
                    int volunteerId = Convert.ToInt32(dr["SenderID"]);
                    int reactionId = Convert.ToInt32(dr["ReactionID"]);
                    DateTime timeStamp = Convert.ToDateTime(dr["Timestamp"].ToString());

                    reactionList.Add(new Reaction(reactionId, questionId, volunteerId, description, volunteerName, timeStamp));
                }

                return reactionList;
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
