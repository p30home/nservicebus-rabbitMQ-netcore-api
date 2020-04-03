using Microsoft.EntityFrameworkCore;

namespace StorageService.Models
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(c=> c.Username).IsUnique(true);
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<ResultHistory> ResultHistories { get; set; }

    }
}