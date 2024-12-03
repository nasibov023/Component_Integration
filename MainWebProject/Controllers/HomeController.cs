using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HashingLibrary;  // Correct namespace for the HashingEncryption class
using System;

namespace MainWebProject.Controllers
{
    public class HomeController : Controller
    {
            private readonly IHttpContextAccessor _httpContextAccessor;
            public HomeController(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                }
                public IActionResult SecureLogin(string username, string password)
    {
        try
        {
            // Simulating a check for null HttpContext or Session
            if (_httpContextAccessor?.HttpContext?.Session == null)
            {
                return Content("Session is not available.");
            }

            // Your normal login logic
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Content("Username or password is missing.");
            }

            // Simulate successful login (replace with actual validation logic)
            if (username == "admin" && password == "securepass")
            {
                _httpContextAccessor.HttpContext.Session.SetString("LoggedInUser", username);
                return Content("Login Successful!");
            }
            else
            {
                return Content("Invalid Username or Password.");
            }
        }
        catch (NullReferenceException ex)
        {
            // Log the error (you can replace this with proper logging)
            return Content($"Exception occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling
            return Content($"An error occurred: {ex.Message}");
        }
    }
        public IActionResult Login()
    {
    return View();  // This will load the Login.cshtml view
    }


        // Action to set a cookie
        public IActionResult SetCookie()
        {
            // Create a cookie
            Response.Cookies.Append("UserProfile", "Shahverdi Nasibov", 
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(10) // Cookie expires in 10 minutes
                });
            return Content("Cookie has been set!");
        }

        // Action to read a cookie
        public IActionResult GetCookie()
        {
            string userProfile = Request.Cookies["UserProfile"];
            return Content(userProfile != null ? $"Cookie Value: {userProfile}" : "No Cookie Found.");
        }

        // Action to set session data
        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("UserPreference", "DarkMode");
            return Content("Session data has been set!");
        }

        // Action to get session data
        public IActionResult GetSession()
        {
            string userPreference = HttpContext.Session.GetString("UserPreference");
            return Content(userPreference != null ? $"Session Value: {userPreference}" : "No Session Data Found.");
        }

        // Action to set TempData
        public IActionResult SetTempData()
        {
            TempData["Message"] = "Welcome to the app!";
            return RedirectToAction("GetTempData");
        }

        // Action to get TempData
        public IActionResult GetTempData()
        {
            string message = TempData["Message"] as string;
            return Content(message != null ? $"TempData Value: {message}" : "No TempData Found.");
        }
    }
}
