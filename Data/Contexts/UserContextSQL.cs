using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Data.Contexts
{
    public class UserContextSql : IUserContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public void LinkCareToProf(int careId, int profId)
        {
            try
            {
                 string query = "INSERT INTO [CareRecipientProfessional] ([CareRecipientID] ,[ProfessionalID]) VALUES  (@careId ,@profId)";


                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@careId", SqlDbType.Int).Value = careId;
                    cmd.Parameters.AddWithValue("@profId", SqlDbType.Int).Value = profId;
                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Linkcare to prof failed");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<User> GetAllProfessionals()
        {
            try
            {
                List<User> professionalList = new List<User>();

                SqlCommand cmd = new SqlCommand("SelectAllProfessionals", _conn);
                cmd.CommandType = CommandType.StoredProcedure;
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow dr in dt.Rows)
                {
                    int userId = Convert.ToInt32(dr["UserID"]);
                    string accountType = dr["AccountType"].ToString();
                    string firstName = dr["FirstName"].ToString();
                    string lastName = dr["LastName"].ToString();
                    DateTime birthDate = Convert.ToDateTime(dr["Birthdate"].ToString());
                    User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), dr["Sex"].ToString());
                    string email = dr["Email"].ToString();
                    string address = dr["Address"].ToString();
                    string postalCode = dr["PostalCode"].ToString();
                    string city = dr["City"].ToString();
                    bool status = Convert.ToBoolean(dr["Status"].ToString());
                    string password = dr["Password"].ToString();

                    User user = new Professional(userId, firstName, lastName, address, city, postalCode, email, birthDate, gender, status, User.AccountType.Professional, password);
                    professionalList.Add(user);
                }

                return professionalList;
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

        public void AddNewUser(User newUser)
        {
            try
            {
                string query = "INSERT INTO [User] (FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Password, AccountType, Status) VALUES (@FirstName, @LastName, @Birthdate, @Sex, @Email, @Address, @PostalCode, @City, @Password, @AccountType, @Status)";
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = newUser.FirstName;
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = newUser.LastName;
                    cmd.Parameters.AddWithValue("@Birthdate", SqlDbType.Date).Value = newUser.BirthDate;
                    cmd.Parameters.AddWithValue("@Sex", SqlDbType.NVarChar).Value = newUser.UserGender.ToString();
                    cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = newUser.EmailAddress;
                    cmd.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = newUser.Address;
                    cmd.Parameters.AddWithValue("@PostalCode", SqlDbType.NChar).Value = newUser.PostalCode;
                    cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = newUser.City;
                    cmd.Parameters.AddWithValue("@Password", SqlDbType.VarChar).Value = newUser.Password;
                    cmd.Parameters.AddWithValue("@AccountType", SqlDbType.NVarChar).Value = newUser.UserAccountType.ToString();
                    cmd.Parameters.AddWithValue("@Status", SqlDbType.Bit).Value = true;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not added");
            }
            finally
            {
                _conn.Close();
            }
        }

        public void EditUser(User currentUser, string password)
        {
            try
            {
                string query = "UPDATE [User] " +
                               "SET FirstName = @FirstName, LastName = @LastName, Birthdate = @Birthdate, Sex = @Sex, Email = @Email, Address = @Address, PostalCode = @PostalCode, City = @City, Status = @Status " +
                               "WHERE UserID = @UserID";
                if (password != "")
                {
                    query = "UPDATE [User] " +
                            "SET FirstName = @FirstName," +
                            " LastName = @LastName, " +
                            "Birthdate = @Birthdate, " +
                            "Sex = @Sex, " +
                            "Email = @Email, " +
                            "Address = @Address, " +
                            "PostalCode = @PostalCode, " +
                            "City = @City, " +
                            "Password = @Password, " +
                            "Status = @Status " +
                            "WHERE UserID = @UserID";
                }

                _conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = currentUser.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = currentUser.LastName;
                    cmd.Parameters.Add("@Birthdate", SqlDbType.DateTime).Value = currentUser.BirthDate;
                    cmd.Parameters.Add("@Sex", SqlDbType.Char).Value = currentUser.UserGender;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = currentUser.EmailAddress;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = currentUser.Address;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NChar).Value = currentUser.PostalCode;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = currentUser.City;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = currentUser.UserId;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                    cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = currentUser.Status;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("User not edited");
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                string query =
                    "SELECT DISTINCT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password, CP.ProfessionalID" +


                    " FROM[User] U" +
                    "  LEFT JOIN CareRecipientProfessional CP ON CP.CareRecipientID = U.UserID ";

                _conn.Open();
                SqlCommand cmd = new SqlCommand(query, _conn);

                List<User> users = new List<User>();


                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                _conn.Close();


                foreach (DataRow dr in dt.Rows)
                {
                    int userId = Convert.ToInt32(dr["UserID"]);
                    string accountType = dr["AccountType"].ToString();
                    string firstName = dr["FirstName"].ToString();
                    string lastName = dr["LastName"].ToString();
                    DateTime birthDate = Convert.ToDateTime(dr["Birthdate"].ToString());
                    User.Gender gender = (User.Gender) Enum.Parse(typeof(User.Gender), dr["Sex"].ToString());
                    string email = dr["Email"].ToString();
                    string address = dr["Address"].ToString();
                    string postalCode = dr["PostalCode"].ToString();
                    string city = dr["City"].ToString();
                    bool status = Convert.ToBoolean(dr["Status"].ToString());
                    string password = dr["Password"].ToString();
                    int professionalId = 0;

                    if (dr["ProfessionalID"] != DBNull.Value)
                    {
                        professionalId = Int32.Parse((dr["ProfessionalID"]).ToString());
                    }

                    if (accountType == "CareRecipient")
                    {
                        if (professionalId != 0)
                        {
                            User professional = GetUserById(professionalId);

                            User user = new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                                birthDate, gender, status, User.AccountType.CareRecipient, password, professional);
                            users.Add(user);
                        }
                        else
                        {
                            User user = new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                                birthDate, gender, status, User.AccountType.CareRecipient, password);
                            users.Add(user);
                        }
                    }

                    else if (accountType == "Volunteer")
                    {
                        User user = new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                            birthDate, gender, status, User.AccountType.Volunteer, password);
                        users.Add(user);
                    }
                    else
                    {
                        User user = new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                            birthDate, gender, status, User.AccountType.Admin, password);
                        users.Add(user);
                    }
                }

                return users;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int GetUserId(string email)
        {
            try
            {
                string query = "SELECT [UserID] FROM [User] WHERE [Email] = @email";
                _conn.Open();

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                int userId = (int) cmd.ExecuteScalar();

                return userId;
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

        public bool CheckIfAccountIsActive(string email)
        {
            try
            {
                string query = "SELECT [Status] FROM [User] WHERE [Email] = @email";
                _conn.Open();

                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";

                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetBoolean(0))
                        {
                            _conn.Close();
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }

                reader.Close();
            }
            finally
            {
                _conn.Close();
            }

            return false;
        }

        public User CheckValidityUser(string emailAdress, string password)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [Email] = @Email";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.SelectCommand = new SqlCommand(query, _conn);

                cmd.SelectCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = emailAdress;
                cmd.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                int userId = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                string accountType = dt.Rows[0].ItemArray[1].ToString();
                string firstName = dt.Rows[0].ItemArray[2].ToString();
                string lastName = dt.Rows[0].ItemArray[3].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[4]);
                User.Gender gender = (User.Gender) Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[5].ToString());
                string address = dt.Rows[0].ItemArray[6].ToString();
                string postalCode = dt.Rows[0].ItemArray[7].ToString();
                string city = dt.Rows[0].ItemArray[8].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[9]);
                string hashedPassword = dt.Rows[0].ItemArray[10].ToString();


                if (!Hasher.SecurePasswordHasher.Verify(password, hashedPassword))
                    throw new ArgumentException("Password invalid");

                switch (accountType)
                {
                    case "CareRecipient":
                        return new CareRecipient(userId, firstName, lastName, address, city, postalCode, emailAdress,
                            birthDate, gender, status, User.AccountType.CareRecipient, hashedPassword);
                    case "Volunteer":
                        return new CareRecipient(userId, firstName, lastName, address, city, postalCode, emailAdress,
                            birthDate, gender, status, User.AccountType.Volunteer, hashedPassword);
                    case "Admin":
                        return new CareRecipient(userId, firstName, lastName, address, city, postalCode, emailAdress,
                            birthDate, gender, status, User.AccountType.Admin, hashedPassword);
                    case "Professional":
                        return new Professional(userId, firstName, lastName, address, city, postalCode, emailAdress,
                            birthDate, gender, status, User.AccountType.Professional, hashedPassword);
                    default:
                        throw new AggregateException("User not found");
                }

            }
            catch (Exception)
            {
                throw new ArgumentException("User cannot be checked");
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool CheckIfUserAlreadyExists(string email)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM [User] WHERE [Email] = @email";

                _conn.Open();

                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@email", email);

                int numberofAccounts = (int)cmd.ExecuteScalar();

                if (numberofAccounts != 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Check failed");
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public User GetUserInfo(string email)
        {
            try
            {
                string query =
                    "SELECT UserID, AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE Email = @email";

                _conn.Open();
                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                SqlCommand cmd = new SqlCommand(query, _conn);
                emailParam.Value = email;
                cmd.Parameters.Add(emailParam);
                User currentUser = new Admin("a", "b", "c,", "d", "e", "f", Convert.ToDateTime("1988/12/20"), User.Gender.Man, true, User.AccountType.CareRecipient, "");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string accountType = reader.GetString(1);
                        User.Gender gender = (User.Gender)Enum.Parse(typeof(User.Gender), reader.GetString(5));


                        if (accountType == "Admin")
                        {
                            currentUser = new Admin(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Admin, reader.GetString(11));
                        }
                        else if (accountType == "Professional")
                        {
                            currentUser = new Professional(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Professional, reader.GetString(11));
                        }
                        else if (accountType == "Volunteer")
                        {
                            currentUser = new Volunteer(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.Volunteer, reader.GetString(11));
                        }
                        else
                        {
                            currentUser = new CareRecipient(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(7), reader.GetString(9), reader.GetString(8), email,
                                reader.GetDateTime(4), gender, reader.GetBoolean(10), User.AccountType.CareRecipient, reader.GetString(11));
                        }
                        return currentUser;
                    }
                    return currentUser;
                }
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

        public User GetUserById(int userId)
        {
            try
            {
                string query =
                    "SELECT AccountType, FirstName, LastName, Birthdate, Sex, Email, Address, PostalCode, City, Status, Password " +
                    "FROM [User] " +
                    "WHERE [UserID] = @UserId";
                _conn.Open();

                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.SelectCommand = new SqlCommand(query, _conn);

                cmd.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                DataTable dt = new DataTable();
                cmd.Fill(dt);

                string accountType = dt.Rows[0].ItemArray[0].ToString();
                string firstName = dt.Rows[0].ItemArray[1].ToString();
                string lastName = dt.Rows[0].ItemArray[2].ToString();
                DateTime birthDate = Convert.ToDateTime(dt.Rows[0].ItemArray[3].ToString());
                User.Gender gender = (User.Gender) Enum.Parse(typeof(User.Gender), dt.Rows[0].ItemArray[4].ToString());
                string email = dt.Rows[0].ItemArray[5].ToString();
                string address = dt.Rows[0].ItemArray[6].ToString();
                string postalCode = dt.Rows[0].ItemArray[7].ToString();
                string city = dt.Rows[0].ItemArray[8].ToString();
                bool status = Convert.ToBoolean(dt.Rows[0].ItemArray[9].ToString());
                string password = dt.Rows[0].ItemArray[10].ToString();


                if (accountType == "CareRecipient")
                {
                    return new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.CareRecipient, password);
                }

                else if (accountType == "Volunteer")
                {
                    return new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Volunteer, password);
                }
                else if (accountType == "Admin")
                {
                    return new CareRecipient(userId, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Admin, password);
                }
                else if (accountType == "Professional")
                {
                    return new Professional(userId, firstName, lastName, address, city, postalCode, email,
                        birthDate, gender, status, User.AccountType.Professional, password);
                }

                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    IdnMapping idn = new IdnMapping();

                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
