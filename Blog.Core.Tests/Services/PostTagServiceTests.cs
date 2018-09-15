using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;
using FluentAssertions;
using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Tests.Services
{
    public class PostTagServiceTests : DatabaseFixture
    {
	    readonly PostTagService _postTagService;

        public PostTagServiceTests() : base("PostTagContext")
        {
            _postTagService = new PostTagService(ContextFactory);
        }

        public async Task should_add_new_post_tag()
        {
            var postTag = new PostTag()
            {
                Post = new Post() { Id = new Guid("{E0448114-1383-423D-AD68-F57EC4355B4E}"), Title = "Post1", Content = "Content1" },
                Tag = new Tag() { Id = new Guid("{D55CB1A7-F073-4F64-9968-4AFE1CAAC798}"), Name = "Tag1" }
            };

            postTag = await _postTagService.AddPostTag(postTag);

            using (var context = ContextFactory.GetNewDbContext())
            {
                var newPostTag = context.FindAsync<PostTag>(postTag.Id);
                newPostTag.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_delete_post_tag()
        {
            var postTag = new PostTag()
            {
                Id = new Guid("{DA6CEA55-610F-4956-913F-D8F409014AC9}"),
                Post = new Post() { Id = new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"), Title = "Post1", Content = "Content1" },
                Tag = new Tag() { Id = new Guid("{552921FC-AA5E-48C4-AEB1-278268DC464C}"), Name = "Tag1" }
            };

            await _postTagService.DeletePostTag(postTag);

            using (var context = ContextFactory.GetNewDbContext())
            {
                postTag = await context.FindAsync<PostTag>(new Guid("{DA6CEA55-610F-4956-913F-D8F409014AC9}"));
                postTag.Should().BeNull();
            }
        }
    }
}
