using Blog.Core.Models;
using Blog.Core.Services.Common;
using System;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class ReplyService
    {
	    readonly BlogContextFactory _contextFactory;

        public ReplyService(BlogContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Reply> AddReply(Reply reply)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                reply.Id = Guid.NewGuid();
                await db.Replies.AddAsync(reply);
                await db.SaveChangesAsync();

                return reply;
            }
        }

        public async Task DeleteReply(Reply reply)
        {
			using (var db = _contextFactory.GetNewDbContext())
			{
                db.Replies.Remove(reply);
                await db.SaveChangesAsync();
            }
        }
    }
}
