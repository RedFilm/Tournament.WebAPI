﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Application.Services;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamController : ControllerBase
	{
		private readonly ITeamService _teamService;
		private readonly TeamModelValidator _teamValidator;

		public TeamController(ITeamService teamService, TeamModelValidator teamValidator)
        {
			_teamService = teamService;
			_teamValidator	= teamValidator;
		}

        [HttpGet("GetTeam/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TeamModel> GetTeam(long id)
		{
			var teamModel = await _teamService.GetTeamByIdAsync(id);

			return teamModel!;
		}

		[HttpGet("GetPlayers/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppUser>))]
		public async Task<IEnumerable<AppUser>> GetPlayers(long id)
		{
			var players = await _teamService.GetTeamPlayersAsync(id);

			return players;
		}

		[HttpGet("GetTeams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TeamModel>> GetTeams()
		{
			var teamModels = await _teamService.GetTeamsAsync();

			return teamModels;
		}

		[HttpGet("GetTournaments/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		public async Task<IEnumerable<TournamentModel>> GetTournaments(long id)
		{
			var tournamentModels = await _teamService.GetTournamentsAsync(id);

			return tournamentModels;
		}

		[HttpPost("CreateTeam")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTeam(TeamModel model)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.CreateTeamAsync(model);

			return Created();
		}

		[HttpPut("UpdateTeam")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTeam(TeamModel model)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.UpdateTeamAsync(model);

			return Ok();
		}

		[HttpDelete("DeleteTeam/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTeam(long id)
		{
			await _teamService.DeleteTeamAsync(id);

			return Ok();
		}

		[HttpPost("AddPlayer/{teamId}/players/{playerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> AddPlayerToTeamAsync(long teamId, long playerId)
		{
			await _teamService.AddPlayerToTeamAsync(teamId, playerId);
			return Ok();
		}

		[HttpDelete("RemovePlayer/{teamId}/players/{playerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> RemovePlayerFromTeamAsync(long teamId, long playerId)
		{
			await _teamService.RemovePlayerFromTeamAsync(teamId, playerId);
			return Ok();
		}

		[HttpPost("RegisterForTournament/{teamId}/tournaments/{tournamentId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> RegisterTeamForTournamentAsync(long teamId, long tournamentId)
		{
			await _teamService.RegisterTeamForTournamentAsync(teamId, tournamentId);
			return Ok();
		}
	}
}