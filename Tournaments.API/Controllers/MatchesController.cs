using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.API.Controllers
{
	[Authorize]
	public class MatchesController
	{
		private readonly IMatchService _matchService;

		public MatchesController(IMatchService matchService)
		{
			_matchService = matchService;
		}

		[HttpGet("{matchId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MatchModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<MatchModel> GetMatch([FromRoute] long matchId)
		{
			return await _matchService.GetMatchByIdAsync(matchId);
		}

		[HttpGet("Stages/{stageId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchModel>))]
		public async Task<IEnumerable<MatchModel>> GetMatches([FromRoute] long stageId)
		{
			return await _matchService.GetMatchesAsync(stageId);
		}
	}
}
