using Microsoft.AspNetCore.Mvc;
using Blog.Core.Services;
using Blog.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class PostTagController : Controller
    {
        private readonly PostTagService _postTagService;

        public PostTagController(PostTagService postTagService)
        {
            _postTagService = postTagService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostTag postTag)
        {
            var result = await _postTagService.AddPostTag(postTag);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PostTag postTag)
        {
            await _postTagService.DeletePostTag(postTag);

            return Ok();
        }
    }
}