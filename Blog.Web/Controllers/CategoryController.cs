using Microsoft.AspNetCore.Mvc;
using Blog.Core.Services;
using Blog.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Blog.Web.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            var result = _categoryService.GetCategories();

            return result;
        }

        [HttpPost]
        public async Task<Category> Post(Category category)
        {
            var result = await _categoryService.AddCategory(category);

            return result;
        }

        [HttpPut]
        public async Task Put(Category category)
        {
            await _categoryService.UpdateCategory(category);
        }

        [HttpDelete]
        public async Task Delete(Category category)
        {
            await _categoryService.DeleteCategory(category);
        }
    }
}