using Admin.Core.Models;
using System.Data.Entity;

namespace Admin.Core.EntityFramework
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext() : base("AdminContext")
        { }

        public DbSet<User> Users { get; set; }
    }
}
