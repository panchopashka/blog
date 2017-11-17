using Blog.Core.Models;
using System.Data.Entity;

namespace Blog.Core.EntityFramework
{
    public class BlogDbContext: DbContext
    {
        public BlogDbContext() : base("BlogContext")
        { }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
