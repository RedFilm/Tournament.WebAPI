using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.BracketModels;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentService _tournamentService;
		private readonly IValidator<TournamentModel> _tournamentValidator;
		private readonly IValidator<BracketUpdateModel> _bracketValidator;

		public TournamentController(ITournamentService tournamentService, 
			IValidator<TournamentModel> tournamentValidator,
			IValidator<BracketUpdateModel> bracketValidator)
		{
			_tournamentService = tournamentService;
			_tournamentValidator = tournamentValidator;
			_bracketValidator = bracketValidator;
		}

		[HttpGet("GetTournament/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentWithIdModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TournamentModel> GetTournament([FromRoute] long tournamentId)
		{
			var tournamentModel = await _tournamentService.GetTournamentByIdAsync(tournamentId);

			return tournamentModel;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TournamentWithIdModel>))]
		public async Task<IEnumerable<TournamentWithIdModel>> GetTournaments()
		{
			var tournamentModels = await _tournamentService.GetTournamentsAsync();

			return tournamentModels;
		}

		[HttpGet("{tournamentId}/GetTeams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TeamModel>> GetTeams([FromRoute] long tournamentId)
		{
			var teamModels = await _tournamentService.GetTeamsAsync(tournamentId);

			return teamModels;
		}

		[HttpPost("CreateTournament")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTournament([FromBody] TournamentModel model)
		{
			await _tournamentValidator.ValidateAndThrowAsync(model);
			await _tournamentService.AddTournamentAsync(model);

			return Created();
		}

		[HttpPut("UpdateTournament/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTournament([FromBody] TournamentModel model, [FromRoute] long tournamentId)
		{
			await _tournamentValidator.ValidateAndThrowAsync(model);
			await _tournamentService.UpdateTournamentAsync(model, tournamentId);

			return Ok();
		}

		[HttpDelete("DeleteTournament/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTournament([FromRoute] long tournamentId)
		{
			await _tournamentService.DeleteTournamentAsync(tournamentId);

			return Ok();
		}

		[HttpPost("{tournamentId}/GenerateNewBracket")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GenerateNewBracket([FromRoute] long tournamentId)
		{
			return await _tournamentService.GenerateNewBracketAsync(tournamentId);
		}

		[HttpPost("{tournamentId}/UpdateBracket")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> UpdateBracket(BracketUpdateModel model)
		{
			await _bracketValidator.ValidateAndThrowAsync(model);

			return await _tournamentService.UpdateBracketAsync(model);
		}

		[HttpPost("{tournamentId}/GetBracket")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BracketModel))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<BracketModel> GetBracket([FromRoute] long tournamentId)
		{
			var bracketModel = await _tournamentService.GetBracketAsync(tournamentId);

			return bracketModel;
		}
	}
}
