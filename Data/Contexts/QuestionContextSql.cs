using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class QuestionContextSql : IQuestionContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void AddQuestion(Question askedQuestion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@status", SqlDbType.VarChar).Value = askedQuestion.Status;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = askedQuestion.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = askedQuestion.Content;
                cmd.Parameters.Add("@urgency", SqlDbType.NVarChar).Value = askedQuestion.Urgency;
                cmd.Parameters.Add("@categoryID", SqlDbType.Int).Value = askedQuestion.CategoryId;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = askedQuestion.CareRecipientId;

                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Question not added");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void DeleteQuestion(Question askedQuestion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QuestionID", SqlDbType.Int).Value = askedQuestion.QuestionId;


                _conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Question not deleted");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<Question> GetAllOpenQuestionsVolunteer()
        {
            try
            {
                List<Question> questionList = new List<Question>();

                SqlCommand cmd = new SqlCommand("SelectAllOpenQuestions", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(0, dr["Name"].ToString(), null);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    string status = dr["Status"].ToString();

                    Question question = status == "Open" ? new Question(questionId, title, content, Question.QuestionStatus.Open, date, urgency, category, careRecipientId) : new Question(questionId, title, content, Question.QuestionStatus.Closed, date, urgency, category, careRecipientId);
                    questionList.Add(question);
                }

                return questionList;
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

        public List<Question> GetAllClosedQuestionsVolunteer(int volunteerId)
        {
            try
            {
                List<Question> questionList = new List<Question>();

                SqlCommand cmd = new SqlCommand("SelectAllClosedQuestionsVolunteer", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VolunteerId", SqlDbType.Int).Value = volunteerId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(0, dr["Name"].ToString(), null);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);

                    questionList.Add(new Question(questionId, title, content, Question.QuestionStatus.Closed, date, urgency, category, careRecipientId));
                }

                return questionList;
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

        public List<Question> GetAllQuestionsProfessional(int userid, string statusrequest)
        {
            try
            {
                List<Question> questionList = new List<Question>();

                SqlCommand cmd = new SqlCommand("SelectAllQuestionsProfessional", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userid;

                _conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(0, dr["Name"].ToString(), null);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);

                    Question question = new Question(questionId, title, content, Question.QuestionStatus.Open, date, urgency, category, careRecipientId);

                    questionList.Add(question);
                }

                return questionList;
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

        public List<Question> GetAllQuestions()
        {
            try
            {
                List<Question> questionList = new List<Question>();

                SqlCommand cmd = new SqlCommand("SelectAllQuestions", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(0, dr["Name"].ToString(), null);
                    int careRecipientId = Convert.ToInt32(dr["CareRecipientID"]);
                    string status = dr["Status"].ToString();

                    Question question = status == "Open" ? new Question(questionId, title, content, Question.QuestionStatus.Open, date, urgency, category, careRecipientId) : new Question(questionId, title, content, Question.QuestionStatus.Closed, date, urgency, category, careRecipientId);
                    questionList.Add(question);
                }
                return questionList;
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

        public List<Question> GetAllOpenQuestionsCareRecipient(int careRecipientId)
        {
            List<Question> questionList = new List<Question>();

            try
            {
                SqlCommand cmd = new SqlCommand("SelectAllOpenQuestionsCareRecipientID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = careRecipientId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(dr["Name"].ToString());
                    Question.QuestionStatus status = dr["Status"].ToString() == "Open" ? Question.QuestionStatus.Open : Question.QuestionStatus.Closed;

                    questionList.Add(new Question(questionId, title, content, status, date, urgency, category, careRecipientId));
                }
                return questionList;
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

        public List<Question> GetAllClosedQuestionsCareRecipient(int careRecipientId)
        {
            List<Question> questionList = new List<Question>();

            try
            {
                SqlCommand cmd = new SqlCommand("SelectAllClosedQuestionsCareRecipientID", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@careRecipientID", SqlDbType.Int).Value = careRecipientId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int questionId = Convert.ToInt32(dr["QuestionID"]);
                    string title = dr["Title"].ToString();
                    string content = dr["Description"].ToString();
                    DateTime date = Convert.ToDateTime(dr["Datetime"]);
                    bool urgency = Convert.ToBoolean(dr["Urgency"]);
                    Category category = new Category(dr["Name"].ToString());
                    Question.QuestionStatus status = dr["Status"].ToString() == "Open" ? Question.QuestionStatus.Open : Question.QuestionStatus.Closed;

                    questionList.Add(new Question(questionId, title, content, status, date, urgency, category, careRecipientId));
                }
                return questionList;
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

        public Question GetSingleQuestion(int questionId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("GetQuestionById", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = questionId;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());


                int categoryId = Convert.ToInt32(dt.Rows[0]["CategoryId"]);
                string categoryName = dt.Rows[0]["Name"].ToString();
                string categoryDescription = dt.Rows[0]["CDescription"].ToString();
                int careRecipientId = Convert.ToInt32(dt.Rows[0]["CareRecipientID"]);
                string title = dt.Rows[0]["Title"].ToString();
                string content = dt.Rows[0]["QDescription"].ToString();
                bool urgency = Convert.ToBoolean(dt.Rows[0]["Urgency"]);
                DateTime timeStamp = Convert.ToDateTime(dt.Rows[0]["TimeStamp"].ToString());

                Category category = new Category(categoryId, categoryName, categoryDescription);
                return new Question(questionId, title, content, Question.QuestionStatus.Open, timeStamp, urgency, category, careRecipientId);
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

        public void EditQuestion(Question question)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("EditQuestion", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = question.Category.CategoryId;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = question.Title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = question.Content;
                cmd.Parameters.Add("@urgency", SqlDbType.Bit).Value = question.Urgency;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = question.QuestionId;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Question not edited");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void ChangeQuestionStatus(int id, string status)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("EditQuestionStatus", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@questionid", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@questionstatus", SqlDbType.NVarChar).Value = status;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new ArgumentException("Question status not changed");
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
