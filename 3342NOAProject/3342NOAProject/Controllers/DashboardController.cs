using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace _3342NOAProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // SECURITY CHECK: If the Session doesn't have a UserId, kick them back to Login
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // Optional: Pass data for your labels via ViewBag
            ViewBag.NewMatches = 0; // You can pull these from a Manager later
            ViewBag.DateRequests = 0;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Destroy the session
            return RedirectToAction("Index", "Login");
        }
    }
}