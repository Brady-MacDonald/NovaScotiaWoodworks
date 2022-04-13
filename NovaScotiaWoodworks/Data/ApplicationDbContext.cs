using Microsoft.EntityFrameworkCore;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Configure database options from dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Create database tables to hold their respective models
        public DbSet<UserModel> Users { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
    }
}
