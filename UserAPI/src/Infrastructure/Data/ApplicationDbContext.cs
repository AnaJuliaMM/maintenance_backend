using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;

namespace UserAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(); 

            base.OnModelCreating(modelBuilder);
        }
    }

}