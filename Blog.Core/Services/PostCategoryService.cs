using Blog.Core.Models;
using Blog.Core.Services.Common;
using System;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostCategoryService
    {
		readonly BlogContextFactory _contextFactory;

		public PostCategoryService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<PostCategory> AddPostCategory(PostCategory postCategory)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                postCategory.Id = Guid.NewGuid();
	            await db.PostCategories.AddAsync(postCategory);
                await db.SaveChangesAsync();

                return postCategory;
            }
        }

        public async Task DeletePostCategory(PostCategory postCategory)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                db.PostCategories.Remove(postCategory);
                await db.SaveChangesAsync();
            }
        }
    }
}
