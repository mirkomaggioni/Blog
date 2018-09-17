using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Core.Services;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
		private readonly PostService _postService;

		public AdminController(PostService postService)
		{
			_postService = postService;
		}

		[HttpGet]
		public IActionResult Index()
        {
			return View();
        }

		[HttpGet]
		public IActionResult PostsIndex(int offset, bool published)
		{
			var result = _postService.GetPosts(offset, published);

			return View(result);
		}
	}
}