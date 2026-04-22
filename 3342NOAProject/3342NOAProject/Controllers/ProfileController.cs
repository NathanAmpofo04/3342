using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using _3342NOAProject.Models;
using System.IO;

namespace _3342NOAProject.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile/Edit
        [HttpGet]
        public IActionResult Edit()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginForm", "Login");
            }

            ProfileManager pm = new ProfileManager();

            // This now calls the API which JOINs AccountFinal and UserFinal
            Account userProfile = pm.GetProfile(userId.Value, HttpContext);

            if (userProfile == null || userProfile.UserId == 0)
            {
                TempData["Message"] = "Could not load profile data.";
                return RedirectToAction("Dashboard", "Home"); // Or wherever your landing page is
            }

            return View("EditProfile", userProfile);
        }

        [HttpPost]
        public IActionResult Edit(Account updatedAccount, string submitButton)
        {
            ProfileManager pm = new ProfileManager();

            // 1. STRATEGIC FALLBACK
            // Since UserName/Password/Email are in AccountFinal and NOT edited here,
            // we ensure they aren't null before sending to the API.
            if (string.IsNullOrEmpty(updatedAccount.UserName)) updatedAccount.UserName = "HiddenUser";
            if (string.IsNullOrEmpty(updatedAccount.Password)) updatedAccount.Password = "HiddenPass123!";
            if (string.IsNullOrEmpty(updatedAccount.Email)) updatedAccount.Email = "hidden@temple.edu";

            // 2. CLEAN VALIDATION
            // Tell MVC to ignore these fields so it doesn't return 'Invalid' to the View
            ModelState.Remove("UserName");
            ModelState.Remove("Password");
            ModelState.Remove("Email");

            // Handle the Visibility Toggle
            if (submitButton == "ToggleVisibility")
            {
                bool newVisibility = !updatedAccount.IsHidden;
                pm.SetProfileVisibility(updatedAccount.UserId, newVisibility, HttpContext);

                TempData["Message"] = newVisibility ? "Profile is now Hidden." : "Profile is now Public!";
                return RedirectToAction("Edit");
            }

            // 3. API CALL
            // The API UpdateUserProfile now specifically updates only the UserFinal table columns
            bool success = pm.UpdateProfile(updatedAccount, HttpContext);

            if (success)
            {
                TempData["Message"] = "Profile updated successfully!";
            }
            else
            {
                TempData["Message"] = "Error: The API rejected the update. Check your database connections.";
            }

            return View("EditProfile", updatedAccount);
        }

        [HttpPost]


        [HttpGet]
        public IActionResult UploadPhotos()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) return RedirectToAction("LoginForm", "Login");

            // CRITICAL: Put the ID in the ViewBag so the hidden input can find it
            ViewBag.UserId = userId.Value;
            return View();
        }
        [HttpPost]
        [HttpPost]
        [HttpPost]
        public IActionResult ProcessUpload(IFormFile imageFile, string imageTitle, int userId)
        {
            // Fix: Re-assign the userId to ViewBag immediately so if the page 
            // reloads due to an error, the ID isn't lost for the next attempt.
            ViewBag.UserId = userId;

            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    byte[] imageData;
                    using (var ms = new MemoryStream())
                    {
                        imageFile.CopyTo(ms);
                        imageData = ms.ToArray();
                    }

                    ProfileManager pm = new ProfileManager();
                    bool success = pm.AddImageToGallery(userId, imageTitle, imageData, imageFile.ContentType, imageFile.Length);

                    if (success)
                    {
                        TempData["Message"] = "Photo uploaded successfully!";
                    }
                    else
                    {
                        // This happens if the DB returns 0 rows affected
                        TempData["ErrorMessage"] = $"Database rejected the upload for User ID: {userId}.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return View("UploadPhotos");
        }
    }
}