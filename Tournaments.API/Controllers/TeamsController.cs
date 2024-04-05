using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tournaments.Domain.Enums;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamModels;
using Tournaments.Domain.Models.TournamentModels;
using Tournaments.Domain.Validators.TeamModelValidators;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TeamsController : ControllerBase
	{
		private readonly ITeamService _teamService;
		private readonly TeamModelValidator _teamValidator;
		private readonly IValidator<PaginationModel> _paginationValidator;

		public TeamsController(ITeamService teamService,
			TeamModelValidator teamValidator,
			IValidator<PaginationModel> paginationValidator)
		{
			_teamService = teamService;
			_teamValidator = teamValidator;
			_paginationValidator = paginationValidator;
		}

		[AllowAnonymous]
		[HttpGet("{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TeamModel> GetTeam([FromRoute] long teamId)
		{
			var teamModel = await _teamService.GetTeamByIdAsync(teamId);

			return teamModel;
		}

		[HttpGet("{teamId}/Players")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserModel>))]
		public async Task<IEnumerable<UserModel>> GetPlayers([FromRoute] long teamId)
		{
			var players = await _teamService.GetTeamPlayersAsync(teamId);

			return players;
		}

		[AllowAnonymous]
		[HttpGet("GetTeams")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamWithIdModel>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		public async Task<PageModel<TeamWithIdModel>> GetTeams([FromQuery] PaginationModel model)
		{
			await _paginationValidator.ValidateAndThrowAsync(model);
			var pageModel = await _teamService.GetTeamsAsync(model);

			return pageModel;
		}

		[HttpGet("{teamId}/Tournaments")]
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

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TeamCreator)}")]
		[HttpPut("{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTeam([FromBody] TeamModel model, [FromRoute] long teamId)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.UpdateTeamAsync(model, teamId);

			return Ok();
		}

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TeamCreator)}")]
		[HttpDelete("{teamId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTeam([FromRoute] long teamId)
		{
			await _teamService.DeleteTeamAsync(teamId);
			return Ok();
		}

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TeamCreator)}")]
		[HttpPost("{teamId}/Players/{playerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> AddPlayerToTeamAsync([FromRoute] long teamId, [FromRoute] long playerId)
		{
			var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

			if (idClaim == null)
				return BadRequest("Name identifier claim is missing");

			if (!long.TryParse(idClaim?.Value, out long inviterId))
				return BadRequest("Invalid format of NameIdentifier claim.");

			await _teamService.AddPlayerToTeamAsync(teamId, playerId, inviterId);
			return Ok();
		}

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TeamCreator)}")]
		[HttpDelete("{teamId}/Players/{playerId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> RemovePlayerFromTeamAsync([FromRoute] long teamId, [FromRoute] long playerId)
		{
			var idClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

			if (idClaim == null)
				return BadRequest("Name identifier claim is missing");

			if (!long.TryParse(idClaim?.Value, out long inviterId))
				return BadRequest("Invalid format of NameIdentifier claim.");

			await _teamService.RemovePlayerFromTeamAsync(teamId, playerId, inviterId);
			return Ok();
		}

		[Authorize(Roles = $"{nameof(IdentityRoles.Administrator)}, {nameof(IdentityRoles.TeamCreator)}")]
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
