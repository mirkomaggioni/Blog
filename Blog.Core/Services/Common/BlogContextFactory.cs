using Blog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Services.Common
{
    public class BlogContextFactory
    {
	    private readonly DbContextOptions<BlogContext> _options;

	    public BlogContextFactory(DbContextOptions<BlogContext> options)
	    {
		    _options = options;
	    }

        public BlogContext GetNewDbContext()
        {
            return new BlogContext(_options);
        }
    }
}
