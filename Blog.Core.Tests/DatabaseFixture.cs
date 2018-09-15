using Blog.Core.Models;
using Blog.Core.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Blog.Core.Tests
{
    public abstract class DatabaseFixture : IDisposable
    {
	    protected readonly BlogContextFactory ContextFactory;
        readonly DbContextOptions<BlogContext> _options;

        public DatabaseFixture(string databaseName)
        {
            _options = new DbContextOptionsBuilder<BlogContext>().UseInMemoryDatabase(databaseName).Options;
			ContextFactory = new BlogContextFactory(_options);

            var categories = new List<Category>()
                {
                    new Category() { Id = new Guid("{5D134CF2-EF37-4663-B3D2-64F2E3DBF3BE}"), Name = "Category1" },
                    new Category() { Id = new Guid("{BE7EB1E1-FB67-4FE1-A96E-4721D37AFE29}"), Name = "Category2" },
                    new Category() { Id = new Guid("{BBB5D5DD-A746-4A87-9204-90EE87B5F560}"), Name = "Category3" }
                };

            var tags = new List<Tag>()
                {
                    new Tag() { Id = new Guid("{859A4C65-F688-4F9F-8575-F389841665F8}"), Name = "Tag1" },
                    new Tag() { Id = new Guid("{A57D65BE-8A33-48E8-A3E4-3EF5D110A961}"), Name = "Tag2" },
                    new Tag() { Id = new Guid("{2D1AC63C-3D48-45A3-8F22-C9670A9D4F84}"), Name = "Tag3" }
                };

            var posts = new List<Post>()
            {
                new Post() { Id = new Guid("{7660E171-0776-451C-AD2C-1CA6F46EDB30}"), Title = "Post1", Content = "Content1", Published = true },
                new Post() { Id = new Guid("{7221B957-E3F2-461F-A4F8-3D13699880A1}"), Title = "Post2", Content = "Content2", Published = false },
                new Post() { Id = new Guid("{D2E934DB-7B64-42A7-953F-213EA771F596}"), Title = "Post3", Content = "Content3", Published = false }
            };

            var replies = new List<Reply>()
            {
                new Reply() { Id = new Guid("{9344DCAF-154E-4167-8EEF-0372896F5469}"), CreateUser = "user1", Content = "Content1", Published = true, Post = posts[0] },
                new Reply() { Id = new Guid("{1DB75F77-A50E-4F90-83F8-711D3025EBA4}"), CreateUser = "user2", Content = "Content2", Published = true, Post = posts[0] },
                new Reply() { Id = new Guid("{6772F1D7-E6D6-47DB-8724-E46353BFDFDD}"), CreateUser = "user3", Content = "Content3", Published = false, Post = posts[0] }
            };

            var postCategories = new List<PostCategory>()
            {
                new PostCategory() {
                    Id = new Guid("{267A0AE0-2E91-4FA5-B45C-361B8AAE635D}"),
                    Post = posts[0],
                    Category = categories[0] }
            };

            var postTags = new List<PostTag>()
            {
                new PostTag() {
                    Id = new Guid("{DA6CEA55-610F-4956-913F-D8F409014AC9}"),
                    Post = posts[0],
                    Tag = tags[0] }
            };

            using (var blogContext = ContextFactory.GetNewDbContext())
            {
                blogContext.Set<Category>().AddRange(categories);
                blogContext.Set<Tag>().AddRange(tags);
                blogContext.Set<Post>().AddRange(posts);
                blogContext.Set<Reply>().AddRange(replies);
                blogContext.Set<PostCategory>().AddRange(postCategories);
                blogContext.Set<PostTag>().AddRange(postTags);
                blogContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var blogContext = ContextFactory.GetNewDbContext())
            {
                blogContext.RemoveRange(blogContext.Categories);
                blogContext.RemoveRange(blogContext.Tags);
                blogContext.RemoveRange(blogContext.Posts);
                blogContext.RemoveRange(blogContext.Replies);
                blogContext.RemoveRange(blogContext.PostCategories);
                blogContext.RemoveRange(blogContext.PostTags);
                blogContext.SaveChanges();
            }
        }
    }
}
