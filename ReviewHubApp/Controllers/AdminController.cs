using Microsoft.AspNetCore.Mvc;
using ReviewHubApp.Data;

namespace ReviewHubApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminDashboard()
        {
            var users = _context.Users.ToList();
            var reviews = _context.Reviews.ToList();
            return View(new { Users = users, Reviews = reviews });
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminDashboard");
        }

        [HttpPost]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
            return RedirectToAction("AdminDashboard");
        }
    }
}
