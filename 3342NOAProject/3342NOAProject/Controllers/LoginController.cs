



using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using _3342NOAProject.Models;

namespace _3342NOAProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            // Safety Check: If the session is empty, kick them back to Login
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginForm");
            }

            // Get the name from the session to display a "Welcome" message
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            return View(); // Looks for Dashboard.cshtml
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult ProcessRegistration(Account acc)
        {
            // 1. Check for the 4-char username, 3-char password, and the No-Numbers Name rule
            if (!ModelState.IsValid)
            {
                // To make the input disappear but keep the error messages:
                // We clear the properties manually.
                acc.UserName = string.Empty;
                acc.Password = string.Empty;
                acc.FullName = string.Empty;
                acc.Email = string.Empty;

                return View("CreateAccount", acc);
            }

            AccountManager manager = new AccountManager();
            int userId = manager.CreateAccount(acc);

            if (userId > 0)
            {
                TempData["Message"] = "Account created successfully!";
                return RedirectToAction("LoginForm");
            }
            else
            {
                TempData["Message"] = "There was an error creating your account.";
                return View("CreateAccount", new Account()); // Total reset on DB error
            }
        }

        [HttpPost]
        public IActionResult ProcessLogin(string UserName, string Password)
        {
            // Note: Since Login usually just takes strings, we check for null/empty here
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                TempData["ErrorMessage"] = "Please enter both a username and password.";
                return View("LoginForm");
            }

            AccountManager manager = new AccountManager();
            int userId = manager.Login(UserName, Password);

            if (userId > 0)
            {
                HttpContext.Session.SetInt32("UserId", userId);
                HttpContext.Session.SetString("UserName", UserName);
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password. Please try again.";
                return View("LoginForm");
            }
        }
    }
}