using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;
using FluentAssertions;
using System;
using Xunit;
using System.Threading.Tasks;

namespace Blog.Core.Tests.Services
{
    public class PostCategoryServiceTests : DatabaseFixture
    {
	    readonly PostCategoryService _postCategoryService;

        public PostCategoryServiceTests() : base("PostCategoryContext")
        {
            _postCategoryService = new PostCategoryService(ContextFactory);
        }

        public async Task should_add_new_post_category()
        {
            var postCategory = new PostCategory()
            {
                Post = new Post() { Id = new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"), Title = "Post1", Content = "Content1" },
                Category = new Category() { Id = new Guid("{9A2E1062-F1D2-45E4-A4ED-A3E35F9046FE}"), Name = "Category1" }
            };

            postCategory = await _postCategoryService.AddPostCategory(postCategory);

            using (var db = ContextFactory.GetNewDbContext())
            {
                var newPostCategory = await db.FindAsync<PostCategory>(postCategory.Id);
                newPostCategory.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_delete_post_category()
        {
            var postCategory = new PostCategory()
            {
                Id = new Guid("{267A0AE0-2E91-4FA5-B45C-361B8AAE635D}"),
                Post = new Post() { Id = new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"), Title = "Post1", Content = "Content1" },
                Category = new Category() { Id = new Guid("{260CFBCE-7B8A-4B8C-AE72-39F71F7597D2}"), Name = "Category1" }
            };

            await _postCategoryService.DeletePostCategory(postCategory);

            using (var db = ContextFactory.GetNewDbContext())
            {
                postCategory = await db.FindAsync<PostCategory>(new Guid("{267A0AE0-2E91-4FA5-B45C-361B8AAE635D}"));
                postCategory.Should().BeNull();
            }
        }
    }
}
