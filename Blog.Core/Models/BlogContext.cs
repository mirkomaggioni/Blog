using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Models
{
    public class BlogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
    }
}
