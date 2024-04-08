using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
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

		[HttpPost("Create")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateJoinRequest([FromBody] TeamJoinRequestModel model)
		{
			await _teamJoinRequestService.CreateJoinRequestAsync(model);
			return Created();
		}

		[HttpGet("{joinRequestId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamJoinRequestWithIdModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TeamJoinRequestWithIdModel> GetJoinRequest([FromRoute] long joinRequestId)
		{
			return await _teamJoinRequestService.GetRequestByIdAsync(joinRequestId);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageModel<TeamJoinRequestWithIdModel>))]
		public async Task<PageModel<TeamJoinRequestWithIdModel>> GetJoinRequests([FromBody] PaginationModel model)
		{
			return await _teamJoinRequestService.GetAllAsync(model);
		}
	}
}
