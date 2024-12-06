using Microsoft.EntityFrameworkCore;
using ReviewHubApp.Models;

namespace ReviewHubApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
