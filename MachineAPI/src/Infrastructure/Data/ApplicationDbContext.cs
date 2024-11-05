using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;

namespace MachineAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Machine> Machines { get; set; }
    }

}