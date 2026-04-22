using System.Data;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Utilities;

namespace _3342NOAProject.Models
{
    public class ProfileManager
    {
        // This helper method finds the API URL automatically on any machine
        // Change this temporarily in the Web Project's ProfileManager
        private string GetUrl(HttpContext context) => "http://localhost:5298/api/Profile";

        public Account GetProfile(int userId, HttpContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync($"{GetUrl(context)}/{userId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var acc = response.Content.ReadFromJsonAsync<Account>().Result;

                    // DEBUG: If you put a breakpoint here, check if acc.UserName is null.
                    // If it is null, the API is not sending it.
                    return acc;
                }
                return new Account();
            }
        }

        public bool UpdateProfile(Account acc, HttpContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(GetUrl(context), acc).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public bool SetProfileVisibility(int userId, bool status, HttpContext context)
        {
            using (HttpClient client = new HttpClient())
            {
                // Matches the PUT route we set in the API Controller
                var response = client.PutAsync($"{GetUrl(context)}/Visibility/{userId}/{status}", null).Result;
                return response.IsSuccessStatusCode;
            }
        }


        DBConnect objDB = new DBConnect();

        public bool AddImageToGallery(int userId, string title, byte[] data, string type, long length)
        {
            try
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
            catch (SqlException ex)
            {
                // Keep your debug lines so you can see them in VS
                foreach (SqlError error in ex.Errors)
                {
                    System.Diagnostics.Debug.WriteLine($"SQL Error {error.Number}: {error.Message}");
                }

                // --- ADD THIS LINE ---
                // This pushes the error up to the Controller's try-catch block
                throw ex;
            }
        }
    }
    }
