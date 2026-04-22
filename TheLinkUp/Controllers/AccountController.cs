using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace TheLinkUp.Controllers
{
    public class AccountController : Controller
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        public IActionResult Login()
        {
            ViewData["Message"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult ValidateLogin()
        {
            string email = Request.Form["txtEmail"];
            string password = Request.Form["txtPassword"];

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ValidateUser";
            objCommand.Parameters.Clear();

            SqlParameter inputParameter = new SqlParameter("@Email", email);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 100;
            objCommand.Parameters.Add(inputParameter);

            inputParameter = new SqlParameter("@Password", password);
            inputParameter.Direction = ParameterDirection.Input;
            inputParameter.SqlDbType = SqlDbType.VarChar;
            inputParameter.Size = 100;
            objCommand.Parameters.Add(inputParameter);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow record = ds.Tables[0].Rows[0];

                HttpContext.Session.SetInt32("MemberID", int.Parse(record["MemberID"].ToString()));
                HttpContext.Session.SetString("Username", record["Username"].ToString());
                HttpContext.Session.SetString("Email", record["Email"].ToString());

                return RedirectToAction("DisplayEvents", "Events");
            }
            else
            {
                ViewData["Message"] = "Invalid login.";
                return View("Login");
            }
        }

        public IActionResult Register()
        {
            ViewData["Message"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            try
            {
                string username = Request.Form["txtUsername"];
                string password = Request.Form["txtPassword"];
                string email = Request.Form["txtEmail"];

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "CreateMember";
                objCommand.Parameters.Clear();

                SqlParameter inputParameter = new SqlParameter("@Username", username);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                inputParameter.Size = 50;
                objCommand.Parameters.Add(inputParameter);

                inputParameter = new SqlParameter("@Password", password);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                inputParameter.Size = 100;
                objCommand.Parameters.Add(inputParameter);

                inputParameter = new SqlParameter("@Email", email);
                inputParameter.Direction = ParameterDirection.Input;
                inputParameter.SqlDbType = SqlDbType.VarChar;
                inputParameter.Size = 100;
                objCommand.Parameters.Add(inputParameter);

                int result = objDB.DoUpdateUsingCmdObj(objCommand);

                if (result > 0)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewData["Message"] = "Account could not be created.";
                    return View("Register");
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Register");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}