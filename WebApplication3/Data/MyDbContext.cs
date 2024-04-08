using Microsoft.EntityFrameworkCore;
using WebApplication3.Model;

namespace WebApplication3.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
