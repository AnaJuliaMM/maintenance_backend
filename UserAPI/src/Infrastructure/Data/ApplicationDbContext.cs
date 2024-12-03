using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;

namespace UserAPI.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
        public required DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create Role and User 1:n relationship
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            // Persist relationships
            base.OnModelCreating(modelBuilder);
        }
    }
}
