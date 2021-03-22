using DVLD.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DVLD.Models.Officer;

namespace DVLD.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Officer> Officers { get; set; }
    }
}
