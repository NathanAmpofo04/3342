using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Utilities; // Your DBConnect utility

namespace ProfileServiceAPI.Models
{
    public class ProfileManager
    {
        DBConnect objDB = new DBConnect();

        public Account GetProfile(int userId)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserById";
            objCommand.Parameters.AddWithValue("@UserId", userId);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            Account acc = new Account();

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                acc.UserId = Convert.ToInt32(dr["UserId"]);
                // Read from AccountFinal (via Join)
                acc.UserName = dr["UserName"]?.ToString() ?? "";
                acc.Password = dr["Password"]?.ToString() ?? "";
                acc.Email = dr["Email"]?.ToString() ?? "";

                // Read from UserFinal
                acc.IsHidden = dr["IsHidden"] != DBNull.Value && Convert.ToBoolean(dr["IsHidden"]);
                acc.FullName = dr["FullName"]?.ToString() ?? "";
                acc.Age = dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"]) : 0;
                acc.Height = dr["Height"]?.ToString() ?? "";
                acc.Weight = dr["Weight"]?.ToString() ?? "";
                acc.Occupation = dr["Occupation"]?.ToString() ?? "";
                acc.Phone = dr["Phone"]?.ToString() ?? "";
                acc.Address = dr["Address"]?.ToString() ?? "";
                acc.City = dr["City"]?.ToString() ?? "";
                acc.State = dr["State"]?.ToString() ?? "";
                acc.PhotoURL = dr["PhotoURL"]?.ToString() ?? "";
                acc.Description = dr["Description"]?.ToString() ?? "";
                acc.Goals = dr["Goals"]?.ToString() ?? "";
                acc.CommitmentType = dr["CommitmentType"]?.ToString() ?? "";
                acc.FavoriteMovie = dr["FavoriteMovie"]?.ToString() ?? "";
                acc.FavoriteRestaurant = dr["FavoriteRestaurant"]?.ToString() ?? "";
                acc.FavoriteBook = dr["FavoriteBook"]?.ToString() ?? "";
                acc.FavoritePoem = dr["FavoritePoem"]?.ToString() ?? "";
                acc.FavoriteQuote = dr["FavoriteQuote"]?.ToString() ?? "";
            }
            return acc;
        }

        public bool UpdateProfile(Account acc)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdateUserProfile";

            objCommand.Parameters.AddWithValue("@UserId", acc.UserId);
            objCommand.Parameters.AddWithValue("@FullName", acc.FullName);
            objCommand.Parameters.AddWithValue("@Age", (object)acc.Age ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Height", (object)acc.Height ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Weight", (object)acc.Weight ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Occupation", (object)acc.Occupation ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Phone", (object)acc.Phone ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Address", (object)acc.Address ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@City", (object)acc.City ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@State", (object)acc.State ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@PhotoURL", (object)acc.PhotoURL ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Description", (object)acc.Description ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@Goals", (object)acc.Goals ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@CommitmentType", (object)acc.CommitmentType ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@FavoriteMovie", (object)acc.FavoriteMovie ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@FavoriteRestaurant", (object)acc.FavoriteRestaurant ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@FavoriteBook", (object)acc.FavoriteBook ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@FavoritePoem", (object)acc.FavoritePoem ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@FavoriteQuote", (object)acc.FavoriteQuote ?? DBNull.Value);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result > 0;
        }

        // Keep SetProfileVisibility and AddImageToGallery as they were
        // Matches the Visibility toggle in the DB
        public bool SetProfileVisibility(int userId, bool shouldHide)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ToggleProfileVisibility";

            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@HideStatus", shouldHide ? 1 : 0);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result > 0;
        }

        // Matches the Image Upload logic in the DB
        public bool AddImageToGallery(int userId, string title, byte[] data, string type, long length)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "StoreUserImage";

            objCommand.Parameters.AddWithValue("@UserId", userId);
            objCommand.Parameters.AddWithValue("@ImageTitle", (object)title ?? DBNull.Value);
            objCommand.Parameters.AddWithValue("@ImageData", data);
            objCommand.Parameters.AddWithValue("@ImageType", type);
            objCommand.Parameters.AddWithValue("@ImageLength", length);

            int result = objDB.DoUpdateUsingCmdObj(objCommand);
            return result > 0;
        }
    }
}