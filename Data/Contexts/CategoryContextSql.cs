using Data.Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Data.Contexts
{
    public class CategoryContextSql : ICategoryContext
    {
        private readonly SqlConnection _conn = Connection.GetConnection();

        public List<Category> GetAllCategories()
        {
            List<Category> categoryList = new List<Category>();
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCategories", _conn) {CommandType = CommandType.StoredProcedure};
                _conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                foreach (DataRow row in dt.Rows)
                {
                    int categoryId = Convert.ToInt32(row["CategoryID"]);
                    string categoryName = row["Name"].ToString();
                    string categoryDescription = row["Description"].ToString();

                    Category category = new Category(categoryId, categoryName, categoryDescription);
                    categoryList.Add(category);
                }

                return categoryList;
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
