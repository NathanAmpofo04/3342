using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TheLinkUp.Models;

namespace TheLinkUp.Controllers
{
    public class ProfileController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        public IActionResult CreateProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProfile(string txtFullName, string txtAge, string txtHeight,
                                           string txtWeight, string txtOccupation, string txtPhone,
                                           string txtAddress, string txtCity, string txtState,
                                           string txtPhotoURL, string txtDescription, string txtGoals,
                                           string txtCommitmentType, string txtMovie,
                                           string txtRestaurant, string txtBook,
                                           string txtPoem, string txtQuote)
        {
            int memberID = HttpContext.Session.GetInt32("MemberID").Value;

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "CreateProfile";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@MemberID", memberID);
            objCommand.Parameters.AddWithValue("@FullName", txtFullName);
            objCommand.Parameters.AddWithValue("@Age", int.Parse(txtAge));
            objCommand.Parameters.AddWithValue("@Height", txtHeight);
            objCommand.Parameters.AddWithValue("@Weight", int.Parse(txtWeight));
            objCommand.Parameters.AddWithValue("@Occupation", txtOccupation);
            objCommand.Parameters.AddWithValue("@Phone", txtPhone);
            objCommand.Parameters.AddWithValue("@Address", txtAddress);
            objCommand.Parameters.AddWithValue("@City", txtCity);
            objCommand.Parameters.AddWithValue("@State", txtState);
            objCommand.Parameters.AddWithValue("@PhotoURL", txtPhotoURL);
            objCommand.Parameters.AddWithValue("@Description", txtDescription);
            objCommand.Parameters.AddWithValue("@Goals", txtGoals);
            objCommand.Parameters.AddWithValue("@CommitmentType", txtCommitmentType);
            objCommand.Parameters.AddWithValue("@FavoriteMovie", txtMovie);
            objCommand.Parameters.AddWithValue("@FavoriteRestaurant", txtRestaurant);
            objCommand.Parameters.AddWithValue("@FavoriteBook", txtBook);
            objCommand.Parameters.AddWithValue("@FavoritePoem", txtPoem);
            objCommand.Parameters.AddWithValue("@FavoriteQuote", txtQuote);
            objCommand.Parameters.AddWithValue("@IsVisible", 1);

            objDB.DoUpdateUsingCmdObj(objCommand);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ViewProfile(int memberID)
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Profile profile = new Profile();

            objCommand.CommandType = CommandType.Text;
            objCommand.CommandText = @"SELECT MemberID, FullName, Age, Height, Weight, Occupation,
                                      Phone, Address, City, State, PhotoURL, Description, Goals,
                                      CommitmentType, FavoriteMovie, FavoriteRestaurant, FavoriteBook,
                                      FavoritePoem, FavoriteQuote, IsVisible
                                      FROM Profiles
                                      WHERE MemberID = @MemberID";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@MemberID", memberID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow record = ds.Tables[0].Rows[0];

                profile.MemberID = int.Parse(record["MemberID"].ToString());
                profile.FullName = record["FullName"].ToString();
                profile.Age = int.Parse(record["Age"].ToString());
                profile.Height = record["Height"].ToString();
                profile.Weight = int.Parse(record["Weight"].ToString());
                profile.Occupation = record["Occupation"].ToString();
                profile.Phone = record["Phone"].ToString();
                profile.Address = record["Address"].ToString();
                profile.City = record["City"].ToString();
                profile.State = record["State"].ToString();
                profile.PhotoURL = record["PhotoURL"].ToString();
                profile.Description = record["Description"].ToString();
                profile.Goals = record["Goals"].ToString();
                profile.CommitmentType = record["CommitmentType"].ToString();
                profile.FavoriteMovie = record["FavoriteMovie"].ToString();
                profile.FavoriteRestaurant = record["FavoriteRestaurant"].ToString();
                profile.FavoriteBook = record["FavoriteBook"].ToString();
                profile.FavoritePoem = record["FavoritePoem"].ToString();
                profile.FavoriteQuote = record["FavoriteQuote"].ToString();
            }

            return View("ViewProfile", profile);
        }
    }
}