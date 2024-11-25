using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasIndex(i => i.Id).IsUnique();
        }
    }
}
