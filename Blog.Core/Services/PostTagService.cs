using Blog.Core.Models;
using Blog.Core.Services.Common;
using System;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostTagService
    {
	    readonly BlogContextFactory _contextFactory;

        public PostTagService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<PostTag> AddPostTag(PostTag postTag)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                postTag.Id = Guid.NewGuid();
                await db.PostTags.AddAsync(postTag);


                return postTag;
            }
        }

        public async Task DeletePostTag(PostTag postTag)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                db.PostTags.Remove(postTag);
                await db.SaveChangesAsync();
            }
        }
    }
}
