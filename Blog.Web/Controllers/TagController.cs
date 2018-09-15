using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Services;
using Blog.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var result = _tagService.GetTags();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tag tag)
        {
            var result = await _tagService.AddTag(tag);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Tag tag)
        {
            await _tagService.UpdateTag(tag);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Tag tag)
        {
            await _tagService.DeleteTag(tag);

            return Ok();
        }
    }
}