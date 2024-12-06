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

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                TempData["UserId"] = user.UserId;
                if (user.IsAdmin)
                    return RedirectToAction("AdminDashboard", "Admin");
                else
                    return RedirectToAction("Main", "Review");
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }

        [HttpGet]
        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
    }
}
