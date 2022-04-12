using Microsoft.EntityFrameworkCore;
using NovaScotiaWoodworks.Models;

namespace NovaScotiaWoodworks.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Database table named 'Users' to hold 'UserModel's
        public DbSet<UserModel> Users { get; set; }
    }
}
