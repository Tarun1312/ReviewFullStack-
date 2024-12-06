using Microsoft.AspNetCore.Mvc;
using ReviewHubApp.Data;
using ReviewHubApp.Models;
using System.Linq;

namespace ReviewHubApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? category = null)
        {
            // Fetch reviews based on category
            var reviews = string.IsNullOrEmpty(category)
                ? _context.Reviews.OrderByDescending(r => r.CreatedAt).ToList()
                : _context.Reviews.Where(r => r.Category == category).OrderByDescending(r => r.CreatedAt).ToList();

            // Fetch distinct categories
            var categories = _context.Reviews.Select(r => r.Category).Distinct().ToList();

            // Pass categories and selected category to the view
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = category;

            return View(reviews); // Return reviews to the view
        }
    }
}
