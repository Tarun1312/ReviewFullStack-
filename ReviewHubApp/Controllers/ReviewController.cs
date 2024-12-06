using Microsoft.AspNetCore.Mvc;
using ReviewHubApp.Data;
using ReviewHubApp.Models;

namespace ReviewHubApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Categories()
        {
            // Static list of categories
            var categories = new List<string>
            {
                "Electronic Things",
                "Food",
                "Diseases",
                "Websites",
                "Entertainment",
                "Other"
            };

            return View(categories);
        }

        public IActionResult Main()
        {
            // Static list of categories for navigation
            var categories = new List<string>
            {
                "Electronic Things",
                "Food",
                "Diseases",
                "Websites",
                "Entertainment",
                "Other"
            };

            return View(categories);
        }

        [HttpGet]
        public IActionResult WriteReview(string category)
        {
            // Ensure user is logged in
            if (TempData["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        public IActionResult WriteReview(string category, string content, int rating)
        {
            try
            {
                // Ensure the user is logged in
                if (TempData["UserId"] == null)
                {
                    TempData["ErrorMessage"] = "You must be logged in to submit a review.";
                    return RedirectToAction("Login", "User");
                }

                // Get the logged-in user ID
                var userId = int.Parse(TempData["UserId"].ToString() ?? "0");

                // Create a new review
                var review = new Review
                {
                    Category = category,
                    Content = content,
                    Rating = rating,
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };

                // Log the review details (for debugging)
                Console.WriteLine($"Review Details: Category = {category}, Content = {content}, Rating = {rating}, UserId = {userId}");

                // Add the review to the database and save changes
                _context.Reviews.Add(review);
                _context.SaveChanges();

                // Log success message
                Console.WriteLine("Review saved successfully!");

                // Redirect to the landing page after saving
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error saving review: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while saving your review. Please try again.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                TempData["UserId"] = user.UserId; // Save user info temporarily
                return RedirectToAction("Main", "Review");
            }

            ViewBag.Error = "Invalid login credentials.";
            return View();
        }
    }
}
