using System.Collections.Generic;
using Blog.Core.Models;
using System.Threading.Tasks;
using System;
using Blog.Core.Services.Common;
using System.Linq;

namespace Blog.Core.Services
{
    public class CategoryService
    {
	    readonly BlogContextFactory _contextFactory;

        public CategoryService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                return db.Categories.OrderBy(c => c.Name).ToList();
            }
        }

        public async Task<Category> AddCategory(Category category)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                category.Id = Guid.NewGuid();

	            await db.Categories.AddAsync(category);
	            await db.SaveChangesAsync();

                return category;
            }
        }

        public async Task UpdateCategory(Category category)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
	            db.Categories.Update(category);
	            await db.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(Category category)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
	            db.Categories.Remove(category);
	            await db.SaveChangesAsync();
            }
        }
    }
}
