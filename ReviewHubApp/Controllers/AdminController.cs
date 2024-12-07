using Microsoft.AspNetCore.Mvc;
using ReviewHubApp.Data;
using ReviewHubApp.Models;
using System.Linq;

namespace ReviewHubApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Admin Dashboard
        public IActionResult AdminDashboard()
        {
            // Ensure the logged-in user is an admin
            if (TempData["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userId = int.Parse(TempData["UserId"].ToString());
            var user = _context.Users.Find(userId);
            if (user == null || !user.IsAdmin)
            {
                return RedirectToAction("Login", "User");
            }

            // Fetch data for dashboard
            var dashboardData = new AdminDashboardViewModel
            {
                Users = _context.Users.ToList(),
                Reviews = _context.Reviews.OrderByDescending(r => r.CreatedAt).ToList()
            };

            return View(dashboardData);
        }

        // Delete Review
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

        // Edit Review (GET)
        [HttpGet]
        public IActionResult EditReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // Edit Review (POST)
        [HttpPost]
        public IActionResult EditReview(Review review)
        {
            if (ModelState.IsValid)
            {
                var existingReview = _context.Reviews.Find(review.ReviewId);
                if (existingReview != null)
                {
                    existingReview.Category = review.Category;
                    existingReview.Content = review.Content;
                    existingReview.Rating = review.Rating;
                    _context.SaveChanges();
                }

                return RedirectToAction("AdminDashboard");
            }

            return View(review);
        }

        // Optional: Delete User
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
    }
}