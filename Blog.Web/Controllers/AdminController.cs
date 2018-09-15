using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Core.Services;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
		public IActionResult Index()
        {
			return View();
        }
    }
}