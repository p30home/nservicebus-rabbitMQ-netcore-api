using Microsoft.EntityFrameworkCore;

namespace AuthService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<ResultHistory> ResultHistories { get; set; }

    }
}