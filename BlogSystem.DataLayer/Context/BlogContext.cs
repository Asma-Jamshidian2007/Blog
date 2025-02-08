using Blog_System.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blog_System.DataLayer.Context
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

 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Post_Category"); 

            modelBuilder.Entity<Post>()
                .HasOne(p => p.SubCategory)
                .WithMany(c => c.SubPosts)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Post_SubCategory"); 

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .HasMaxLength(200)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BlogContext(optionsBuilder.Options);
        }
    }
}
