using EFCore_LINQ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_LINQ.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=.;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
