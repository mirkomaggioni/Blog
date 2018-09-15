using Blog.Core.Models;
using Blog.Core.Services;
using Blog.Core.Services.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Core.Tests.Services
{
    public class CategoryServiceTests : DatabaseFixture
    {
	    readonly CategoryService _categoryService;

        public CategoryServiceTests() : base("CategoryContext")
        {
            _categoryService = new CategoryService(ContextFactory);
        }

        [Fact]
        public void should_return_categories_list()
        {
            var categories = _categoryService.GetCategories();
            categories.Should().NotBeNullOrEmpty();
            categories.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task should_add_new_category()
        {
            var category = new Category()
            {
                Id = new Guid("{1225FE5B-9C46-4BA3-9233-9337DDC5F478}"),
                Name = "Test Category"
            };

            category = await _categoryService.AddCategory(category);

            using (var db = ContextFactory.GetNewDbContext())
            {
                var newCategory = await db.FindAsync<Category>(category.Id);
                newCategory.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task should_edit_category()
        {
            using (var db = ContextFactory.GetNewDbContext())
            {
                var category = await db.FindAsync<Category>(new Guid("{BE7EB1E1-FB67-4FE1-A96E-4721D37AFE29}"));
                category.Name = "category modified";
                await _categoryService.UpdateCategory(category);

                category = await db.FindAsync<Category>(new Guid("{BE7EB1E1-FB67-4FE1-A96E-4721D37AFE29}"));
                category.Name.ShouldBeEquivalentTo("category modified");
            }
        }

        [Fact]
        public async Task should_delete_category()
        {
            var category = new Category() { Id = new Guid("{5D134CF2-EF37-4663-B3D2-64F2E3DBF3BE}"), Name = "Category1" };
            await _categoryService.DeleteCategory(category);

            using (var db = ContextFactory.GetNewDbContext())
            {
                category = await db.FindAsync<Category>(new Guid("{5D134CF2-EF37-4663-B3D2-64F2E3DBF3BE}"));
                category.Should().BeNull();
            }
        }
    }
}
