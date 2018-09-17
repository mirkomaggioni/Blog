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
    public class PostController : Controller
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Get(int offset)
		{
			var result = _postService.GetPosts(offset, true);

			return Ok(result);
		}

		[HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            var result = await _postService.AddPost(post);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post post)
        {
            await _postService.UpdatePost(post);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Post post)
        {
            await _postService.DeletePost(post);

            return Ok();
        }
    }
}