using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TestController : ControllerBase
	{
		[HttpGet("AuthorizedOnly")]
		public ActionResult AuthorizedOnly()
		{
			return Ok("you've been authorized!");
		}
	}
}
