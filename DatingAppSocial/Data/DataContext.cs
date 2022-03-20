using DatingAppSocial.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppSocial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> User { get; set; }
    }
}
