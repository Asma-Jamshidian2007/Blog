using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog_System.DataLayer.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        // Define DbSet for Posts table
        public DbSet<Post> Posts { get; set; }

        // Define DbSet for Users table
        public DbSet<User> Users { get; set; }

        // Define DbSet for Categories table
        public DbSet<Category> Categories { get; set; }

        // Define DbSet for PostComments table
        public DbSet<PostComments> PostComments { get; set; }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(
                "Server=.\\DEV_SQLSERVER;Database=BlogDB;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new BlogContext(optionsBuilder.Options);
        }
    }
}
