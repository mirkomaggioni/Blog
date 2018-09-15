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
    public class TagServiceTests : DatabaseFixture
    {
	    readonly TagService _tagService;

        public TagServiceTests() : base("TagContext")
        {
            _tagService = new TagService(ContextFactory);
        }

        [Fact]
        public void should_return_tags_list()
        {
            var tags = _tagService.GetTags();
            tags.Should().NotBeNullOrEmpty();
            tags.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task should_add_new_tag()
        {
            var tag = new Tag()
            {
                Id = new Guid("{2AE1EF05-D0D4-4D1C-95F0-AB79FAE9E930}"),
                Name = "Test Tag"
            };

            tag = await _tagService.AddTag(tag);

            using (var context = ContextFactory.GetNewDbContext())
            {
                var newTag = await context.FindAsync<Tag>(tag.Id);
                newTag.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_edit_tag()
        {
            using (var context = ContextFactory.GetNewDbContext())
            {
                var tag = await context.FindAsync<Tag>(new Guid("{859A4C65-F688-4F9F-8575-F389841665F8}"));
                tag.Name = "tag modified";
                await _tagService.UpdateTag(tag);

                tag = await context.FindAsync<Tag>(new Guid("{859A4C65-F688-4F9F-8575-F389841665F8}"));
                tag.Name.ShouldBeEquivalentTo("tag modified");
            }
        }

        [Fact]
        public async Task should_delete_tag()
        {
            var tag = new Tag() { Id = new Guid("{A57D65BE-8A33-48E8-A3E4-3EF5D110A961}"), Name = "Tag2" };
            await _tagService.DeleteTag(tag);

            using (var context = ContextFactory.GetNewDbContext())
            {
                tag = await context.FindAsync<Tag>(new Guid("{A57D65BE-8A33-48E8-A3E4-3EF5D110A961}"));
                tag.Should().BeNull();
            }
        }
    }
}
