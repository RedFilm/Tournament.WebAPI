using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models;
using FluentValidation;
using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BracketController : ControllerBase
	{
		private readonly IBracketService _bracketService;
		private readonly IValidator<BracketUpdateModel> _bracketValidator;

        public BracketController(IBracketService bracketService,
			IValidator<BracketUpdateModel> bracketValidator)
        {
            _bracketService = bracketService;
			_bracketValidator = bracketValidator;
        }

		[HttpPost("GetBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GetBracket([FromRoute] long tournamentId)
		{
			return await _bracketService.GetBracketAsync(tournamentId);
		}

		[HttpPost("GenerateNewBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GenerateNewBracket([FromRoute] long tournamentId)
		{
			return await _bracketService.GenerateNewBracketAsync(tournamentId);
		}

		[HttpPost("UpdateBracket/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> UpdateBracket([FromRoute] long tournamentId, [FromBody] IList<MatchResultModel> matchResultModels)
		{
			return await _bracketService.UpdateBracketAsync(matchResultModels, tournamentId);
		}
	}
}
