using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;

namespace MachineAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   

            // Create Machine and Category 1:n relationship
            modelBuilder.Entity<Machine>()
                .HasOne(machine => machine.Category)
                .WithMany(category => category.Machines)
                .HasForeignKey(machine => machine.CategoryId);

            // Create Machine and Location 1:n relationship
            modelBuilder.Entity<Machine>()
                .HasOne(machine => machine.Location)
                .WithMany(location => location.Machines)
                .HasForeignKey(machine => machine.LocationId);

            // Persist relationships
            base.OnModelCreating(modelBuilder);
        }

    }

}