using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;
using Tournaments.Domain.Validators.TeamModelValidators;

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
			_teamValidator = teamValidator;
		}

		[HttpGet("GetTeam/{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TeamModel> GetTeam([FromRoute] long teamId)
		{
			var teamModel = await _teamService.GetTeamByIdAsync(teamId);

			return teamModel;
		}

		[HttpGet("{teamId}/GetPlayers")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
		public async Task<IEnumerable<UserModel>> GetPlayers([FromRoute] long teamId)
		{
			var players = await _teamService.GetTeamPlayersAsync(teamId);

			return players;
		}

		[HttpGet("GetTeams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamWithIdModel>))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<IEnumerable<TeamWithIdModel>> GetTeams()
		{
			var teamModels = await _teamService.GetTeamsAsync();

			return teamModels;
		}

		[HttpGet("{teamId}/GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TournamentWithIdModel>))]
		public async Task<IEnumerable<TournamentWithIdModel>> GetTournaments([FromRoute] long teamId)
		{
			var tournamentModels = await _teamService.GetTournamentsAsync(teamId);

			return tournamentModels;
		}

		[HttpPost("CreateTeam")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTeam([FromBody] TeamModel model)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.CreateTeamAsync(model);

			return Created();
		}

		[HttpPut("UpdateTeam/{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTeam([FromBody] TeamModel model, [FromRoute] long teamId)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.UpdateTeamAsync(model, teamId);

			return Ok();
		}

		[HttpDelete("DeleteTeam/{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTeam([FromRoute] long teamId)
		{
			await _teamService.DeleteTeamAsync(teamId);
			return Ok();
		}

		[HttpPost("AddPlayer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> AddPlayerToTeamAsync([FromBody] TeamMemberUpdateModel model)
		{
			await _teamService.AddPlayerToTeamAsync(model);
			return Ok();
		}

		[HttpDelete("RemovePlayer")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> RemovePlayerFromTeamAsync([FromBody] TeamMemberUpdateModel model)
		{
			await _teamService.RemovePlayerFromTeamAsync(model);
			return Ok();
		}

		[HttpPost("RegisterForTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> RegisterTeamForTournamentAsync([FromBody] RegisterForTournamentModel model)
		{
			await _teamService.RegisterTeamForTournamentAsync(model);
			return Ok();
		}
	}
}
