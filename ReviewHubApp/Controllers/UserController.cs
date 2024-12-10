using Microsoft.AspNetCore.Mvc;
using ReviewHubApp.Data;
using ReviewHubApp.Models;

namespace ReviewHubApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                // Store UserId in TempData for session tracking
                TempData["UserId"] = user.UserId;

                // Redirect based on user type
                if (user.IsAdmin)
                    return RedirectToAction("AdminDashboard", "Admin");
                else
                    return RedirectToAction("Main", "Review");
            }

            // If credentials are invalid, show error message
            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        // GET: Signup Page
        [HttpGet]
        public IActionResult Signup() => View();

        // POST: Signup
        [HttpPost]
        public IActionResult Signup(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        // GET: Logout
        [HttpGet]
        public IActionResult Logout()
        {
            // Clear TempData to remove user session
            TempData.Clear();


            // Redirect to Login page after logout
            return RedirectToAction("Login");
        }
    }
}
