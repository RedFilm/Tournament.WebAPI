using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Tournaments.Application.Services;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentService _tournamentSevice;
		private readonly IValidator<TournamentWithIdModel> _touranmentValidator;

		public TournamentController(ITournamentService tournamentService, IValidator<TournamentWithIdModel> touranmentValidator)
        {
            _tournamentSevice = tournamentService;
			_touranmentValidator = touranmentValidator;

		}

		[HttpGet("GetTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentWithIdModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TournamentModel> GetTournament(long id)
		{
			var tournamentModel = await _tournamentSevice.GetTournamentByIdAsync(id);

			return tournamentModel!;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TournamentWithIdModel>))]
		public async Task<IEnumerable<TournamentWithIdModel>> GetTournaments()
		{
			var tournamentModels = await _tournamentSevice.GetTournamentsAsync();

			return tournamentModels;
		}

		[HttpGet("GetTeams/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TeamModel>> GetTeams(long id)
		{
			var teamModels = await _tournamentSevice.GetTeamsAsync(id);

			return teamModels;
		}

		[HttpPost("CreateTournament")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTournament(TournamentWithIdModel model)
		{
			await _touranmentValidator.ValidateAndThrowAsync(model);
			await _tournamentSevice.AddTournamentAsync(model);

			return Created();
		}

		[HttpPut("UpdateTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTournament(TournamentWithIdModel model)
		{
			await _touranmentValidator.ValidateAndThrowAsync(model);
			await _tournamentSevice.UpdateTournamentAsync(model);

			return Ok();
		}

		[HttpDelete("DeleteTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTournament(long id)
		{
			await _tournamentSevice.DeleteTournamentAsync(id);

			return Ok();
		}
	}
}
