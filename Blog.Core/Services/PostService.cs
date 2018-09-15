using Blog.Core.Models;
using Blog.Core.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PostService
    {
	    readonly BlogContextFactory _contextFactory;

        public PostService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<Post> GetPosts(int offset, bool? published)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                return db.Posts.Where(p => (published == null) || (p.Published == published)).OrderByDescending(p => p.ModifyDate).Skip(offset).Take(10).ToList();
            }
        }

        public async Task<Post> AddPost(Post post)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                post.Id = Guid.NewGuid();
                post.Published = false;
	            await db.Posts.AddAsync(post);
                await db.SaveChangesAsync();

                return post;
            }
        }

        public async Task UpdatePost(Post post)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
	            db.Posts.Update(post);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeletePost(Post post)
        {
            using (var db = _contextFactory.GetNewDbContext())
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
            }
        }
    }
}
