using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentService _tournamentSevice;
		private readonly IValidator<TournamentModel> _touranmentValidator;

		public TournamentController(ITournamentService tournamentService, IValidator<TournamentModel> touranmentValidator)
        {
            _tournamentSevice = tournamentService;
			_touranmentValidator = touranmentValidator;

		}

		[HttpGet("GetTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TournamentModel))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionResponseModel))]
		public async Task<TournamentModel> GetTournament(long id)
		{
			var tournamentModel = await _tournamentSevice.GetTournamentByIdAsync(id);

			return tournamentModel!;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TournamentModel>))]
		public async Task<IEnumerable<TournamentModel>> GetTournaments()
		{
			var tournamentModels = await _tournamentSevice.GetTournamentsAsync();

			return tournamentModels;
		}

		[HttpPost("CreateTournament")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> CreateTournament(TournamentModel model)
		{
			await _touranmentValidator.ValidateAndThrowAsync(model);
			await _tournamentSevice.AddTournamentAsync(model);

			return Created();
		}

		[HttpPut("UpdateTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionResponseModel))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionResponseModel))]
		public async Task<IActionResult> UpdateTournament(TournamentModel model)
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
