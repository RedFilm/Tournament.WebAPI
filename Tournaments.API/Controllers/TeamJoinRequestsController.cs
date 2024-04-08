using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class TeamJoinRequestsController : ControllerBase
	{
		private readonly ITeamJoinRequestService _teamJoinRequestService;
		public TeamJoinRequestsController(ITeamJoinRequestService teamJoinRequestService)
		{
			_teamJoinRequestService = teamJoinRequestService;
		}

		[HttpPost("Send")]
		public async Task<IActionResult> SendJoinRequest([FromBody] TeamJoinRequestModel model)
		{
			await _teamJoinRequestService.CreateJoinRequestAsync(model);
			return Ok();
		}
	}
}
