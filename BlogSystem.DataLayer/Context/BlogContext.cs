using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace   Blog_System.DataLayer.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        { 
        
        }
         public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostComments> PostComments { get; set; }
        public class YourDbContextFactory : IDesignTimeDbContextFactory<BlogContext>
        {
            public BlogContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BlogDb;Trusted_Connection=True;MultipleActiveResultSets=true;");

                return new BlogContext(optionsBuilder.Options);
            }
        }
    }
}
       
