using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Core.Tests.Services
{
    public class PostServiceTests : DatabaseFixture
    {
	    readonly PostService _postService;

        public PostServiceTests() : base("PostContext")
        {
            _postService = new PostService(ContextFactory);
        }

        [Fact]
        public void should_return_posts_list()
        {
            var posts = _postService.GetPosts(0, null);
            posts.Should().NotBeNullOrEmpty();
            posts.Count().Should().BeGreaterThan(0);
            posts.Count().Should().BeLessThan(11);
        }

        [Fact]
        public void should_return_posts_published_list()
        {
            var posts = _postService.GetPosts(0, true);
            posts.Should().NotBeNullOrEmpty();
            posts.Count().Should().BeGreaterThan(0);
            posts.Count(p => !p.Published).ShouldBeEquivalentTo(0);
        }

        [Fact]
        public async Task should_add_new_post()
        {
            var post = new Post()
            {
                Id = new Guid("{2AE1EF05-D0D4-4D1C-95F0-AB79FAE9E930}"),
                Title = "Test Post"
            };

            post = await _postService.AddPost(post);

            using (var context = ContextFactory.GetNewDbContext())
            {
                var newPost = await context.FindAsync<Post>(post.Id);
                newPost.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_edit_post()
        {
            using (var context = ContextFactory.GetNewDbContext())
            {
                var post = await context.FindAsync<Post>(new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"));
                post.Title = "post modified";
                await _postService.UpdatePost(post);

                post = await context.FindAsync<Post>(new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"));
                post.Title.ShouldBeEquivalentTo("post modified");
            }
        }

        [Fact]
        public async Task should_delete_post()
        {
            var post = new Post() { Id = new Guid("{7221B957-E3F2-461F-A4F8-3D13699880A1}"), Title = "Post2" };
            await _postService.DeletePost(post);

            using (var context = ContextFactory.GetNewDbContext())
            {
                post = await context.FindAsync<Post>(new Guid("{7221B957-E3F2-461F-A4F8-3D13699880A1}"));
                post.Should().BeNull();
            }
        }
    }
}
