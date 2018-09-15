using Microsoft.AspNetCore.Mvc;
using Blog.Core.Services;
using Blog.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReplyController : Controller
    {
        private readonly ReplyService _replyService;

        public ReplyController(ReplyService replyService)
        {
            _replyService = replyService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reply reply)
        {
            var result = await _replyService.AddReply(reply);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Reply reply)
        {
            await _replyService.DeleteReply(reply);

            return Ok();
        }
    }
}