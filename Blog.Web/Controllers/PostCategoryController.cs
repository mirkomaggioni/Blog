using Microsoft.AspNetCore.Mvc;
using Blog.Core.Services;
using Blog.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class PostCategoryController : Controller
    {
        private readonly PostCategoryService _postCategoryService;

        public PostCategoryController(PostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCategory postCategory)
        {
            var result = await _postCategoryService.AddPostCategory(postCategory);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PostCategory postCategory)
        {
            await _postCategoryService.DeletePostCategory(postCategory);

            return Ok();
        }
    }
}