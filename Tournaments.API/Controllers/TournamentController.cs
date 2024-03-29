﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentService _tournamentService;
		private readonly IValidator<TournamentWithIdModel> _tournamentValidator;

		public TournamentController(ITournamentService tournamentService, IValidator<TournamentWithIdModel> tournamentValidator)
		{
			_tournamentService = tournamentService;
			_tournamentValidator = tournamentValidator;

		}

		[HttpGet("GetTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentWithIdModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TournamentModel> GetTournament(long id)
		{
			var tournamentModel = await _tournamentService.GetTournamentByIdAsync(id);

			return tournamentModel;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TournamentWithIdModel>))]
		public async Task<IEnumerable<TournamentWithIdModel>> GetTournaments()
		{
			var tournamentModels = await _tournamentService.GetTournamentsAsync();

			return tournamentModels;
		}

		[HttpGet("{id}/GetTeams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TeamModel>> GetTeams(long id)
		{
			var teamModels = await _tournamentService.GetTeamsAsync(id);

			return teamModels;
		}

		[HttpPost("CreateTournament")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTournament(TournamentWithIdModel model)
		{
			await _tournamentValidator.ValidateAndThrowAsync(model);
			await _tournamentService.AddTournamentAsync(model);

			return Created();
		}

		[HttpPut("UpdateTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTournament(TournamentWithIdModel model)
		{
			await _tournamentValidator.ValidateAndThrowAsync(model);
			await _tournamentService.UpdateTournamentAsync(model);

			return Ok();
		}

		[HttpDelete("DeleteTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTournament(long id)
		{
			await _tournamentService.DeleteTournamentAsync(id);

			return Ok();
		}
	}
}
