using FluentValidation;
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

		//[HttpGet("GetTeams")]
		//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamModel>))]
		//public async Task<IEnumerable<TeamModel>> GetTeams()
		//{
		//	var teamModels = await _teamService.GetTeamsAsync();

		//	return teamModels;
		//}

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
		public async Task<IActionResult> UpdateTournament(TeamModel model)
		{
			await _teamValidator.ValidateAndThrowAsync(model);
			await _teamService.UpdateTeamAsync(model);

			return Ok();
		}

		[HttpDelete("DeleteTeam/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> DeleteTournament(long id)
		{
			await _teamService.DeleteTeamAsync(id);

			return Ok();
		}
	}
}
