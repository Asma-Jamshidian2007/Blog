using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Blog_System.DataLayer.Context
{
    public class BlogContext : DbContext
    {
        // Constructor to configure DbContext options
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

    // Factory class to create DbContext at design time
    public class DbContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            // Configure DbContext with SQL Server connection string
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(
                "Server=.\\DEV_SQLSERVER;Database=BlogDB;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new BlogContext(optionsBuilder.Options);
        }
    }
}
