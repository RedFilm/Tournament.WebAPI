using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Enums;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentsController : ControllerBase
	{
		private readonly ITournamentService _tournamentService;
		private readonly IValidator<TournamentModel> _tournamentValidator;
		private readonly IValidator<PaginationModel> _paginationValidator;

		public TournamentsController(ITournamentService tournamentService,
			IValidator<TournamentModel> tournamentValidator,
			IValidator<PaginationModel> paginationValidator)
		{
			_tournamentService = tournamentService;
			_tournamentValidator = tournamentValidator;
			_paginationValidator = paginationValidator;
		}

		[HttpGet("{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentWithIdModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TournamentModel> GetTournament([FromRoute] long tournamentId)
		{
			var tournamentModel = await _tournamentService.GetTournamentByIdAsync(tournamentId);

			return tournamentModel;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageModel<TournamentWithIdModel>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<PageModel<TournamentWithIdModel>> GetTournaments([FromQuery] PaginationModel model)
		{
			await _paginationValidator.ValidateAndThrowAsync(model);
			var pageModel = await _tournamentService.GetTournamentsAsync(model);

			return pageModel;
		}

		[HttpGet("{tournamentId}/Teams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TeamModel>> GetTeams([FromRoute] long tournamentId)
		{
			var teamModels = await _tournamentService.GetTeamsAsync(tournamentId);

			return teamModels;
		}

		[Authorize]
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

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TournamentCreator)}")]
		[HttpPut("{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTournament([FromBody] TournamentModel model, [FromRoute] long tournamentId)
		{
			await _tournamentValidator.ValidateAndThrowAsync(model);
			await _tournamentService.UpdateTournamentAsync(model, tournamentId);

			return Ok();
		}

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TournamentCreator)}")]
		[HttpDelete("{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTournament([FromRoute] long tournamentId)
		{
			await _tournamentService.DeleteTournamentAsync(tournamentId);

			return Ok();
		}
	}
}
