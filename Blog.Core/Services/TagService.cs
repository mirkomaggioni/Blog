using System.Collections.Generic;
using Blog.Core.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Blog.Core.Services.Common;

namespace Blog.Core.Services
{
    public class TagService
    {
	    readonly BlogContextFactory _contextFactory;

        public TagService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;

        }

        public IEnumerable<Tag> GetTags()
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                return db.Tags.OrderBy(t => t.Name).ToList();           }
        }

        public async Task<Tag> AddTag(Tag tag)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                tag.Id = Guid.NewGuid();
				await db.Tags.AddAsync(tag);
                await db.SaveChangesAsync();

                return tag;
            }
        }

        public async Task UpdateTag(Tag tag)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                db.Tags.Update(tag);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteTag(Tag tag)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                db.Remove(tag);
                await db.SaveChangesAsync();
            }
        }
    }
}
