using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models.TeamModels;

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
		public async Task<BracketModel> GetMatch([FromRoute] long matchId)
		{
			return await _matchService.GetBracketAsync(matchId);
		}

		[HttpGet("{bracketId}/Stages/{stageId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MatchModel>))]
		public async Task<IEnumerable<TeamWithIdModel>> GetTeams([FromRoute] long bracketId, [FromRoute] long stageId)
		{
			return await _matchService.GetMatchesAsync();
		}
	}
}
