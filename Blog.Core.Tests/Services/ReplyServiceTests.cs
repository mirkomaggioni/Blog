using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Core.Tests.Services
{
    public class ReplyServiceTests : DatabaseFixture
    {
	    readonly ReplyService _replyService;

        public ReplyServiceTests() : base("ReplyContext")
        {
            _replyService = new ReplyService(ContextFactory);
        }

        [Fact]
        public async Task should_add_new_reply()
        {
            var reply = new Reply()
            {
                Id = new Guid("{83EF2585-5B43-4112-AD77-C5859ADB2B61}"),
                Content = "Test Reply"
            };

            reply = await _replyService.AddReply(reply);

            using (var context = ContextFactory.GetNewDbContext())
            {
                var newReply = await context.FindAsync<Reply>(reply.Id);
                newReply.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_delete_reply()
        {
            var reply = new Reply() { Id = new Guid("{6772F1D7-E6D6-47DB-8724-E46353BFDFDD}"), Content = "Content3" };
            await _replyService.DeleteReply(reply);

            using (var context = ContextFactory.GetNewDbContext())
            {
                reply = await context.FindAsync<Reply>(new Guid("{6772F1D7-E6D6-47DB-8724-E46353BFDFDD}"));
                reply.Should().BeNull();
            }
        }
    }
}
