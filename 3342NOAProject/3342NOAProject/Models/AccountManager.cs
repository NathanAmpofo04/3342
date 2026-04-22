using System;
using System.Data;
using Microsoft.Data.SqlClient; // Use Microsoft.Data.SqlClient for Core
using Utilities;

using _3342NOAProject.Models;

namespace _3342NOAProject.Models
{
    public class AccountManager
    {
        DBConnect objDB = new DBConnect();

        public int CreateAccount(Account acc)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CreateAccountFinal";

            objCommand.Parameters.AddWithValue("@UserName", acc.UserName);
            objCommand.Parameters.AddWithValue("@Password", acc.Password);
            objCommand.Parameters.AddWithValue("@Email", acc.Email);
            objCommand.Parameters.AddWithValue("@FullName", acc.FullName);

            try
            {
                // Use GetDataSet so we can actually READ the ID returned by the SELECT statement
                DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // This grabs the ID from the "SELECT @NewUserId" line in your Proc
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }

                return -1; // If no ID came back
            }
            catch (Exception ex)
            {
                throw new Exception("Manager Error: " + ex.Message);
            }
        }
        public int Login(string username, string password)
        {
            try
            {
                SqlCommand objCommand = new SqlCommand();
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "LoginSPfinal";

                objCommand.Parameters.AddWithValue("@UserName", username);
                objCommand.Parameters.AddWithValue("@Password", password);

                SqlParameter outputId = new SqlParameter("@UserId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                objCommand.Parameters.Add(outputId);

                objDB.DoUpdateUsingCmdObj(objCommand);

                if (outputId.Value != DBNull.Value)
                {
                    return Convert.ToInt32(outputId.Value);
                }

                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}