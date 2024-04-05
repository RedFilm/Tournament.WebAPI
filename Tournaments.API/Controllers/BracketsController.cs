using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models;
using Tournaments.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Tournaments.Domain.Enums;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TournamentCreator)}")]
	public class BracketsController : ControllerBase
	{
		private readonly IBracketService _bracketService;

		public BracketsController(IBracketService bracketService)
		{
			_bracketService = bracketService;
		}

		[AllowAnonymous]
		[HttpGet("GetBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GetBracket([FromRoute] long tournamentId)
		{
			return await _bracketService.GetBracketAsync(tournamentId);
		}

		[HttpPost("GenerateNewBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GenerateNewBracket([FromRoute] long tournamentId, [FromQuery] bool shuffleTeams = true)
		{
			return await _bracketService.GenerateNewBracketAsync(tournamentId, shuffleTeams);
		}

		[HttpPut("UpdateBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> UpdateBracket([FromRoute] long tournamentId, [FromBody] IEnumerable<MatchResultModel> matchResultModels)
		{
			return await _bracketService.UpdateBracketAsync(matchResultModels, tournamentId);
		}
	}
}
