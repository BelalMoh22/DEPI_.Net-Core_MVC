using MVCDemoLabpart1.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCDemoLabpart1.Data
{
    public class MVCDbContext : DbContext // Inherit from DbContext
    {
        public MVCDbContext()
        {
            
        }

        public MVCDbContext(DbContextOptions<MVCDbContext> options) : base(options) // Now valid
        {
            
        }

        public DbSet<Category> Categories { get; set; } // DbSet for Categories
        public DbSet<Products> Products { get; set; } // DbSet for Products
        public DbSet<User> Users { get; set; } // DbSet for Users
    }
}
