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
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tournament))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<TournamentModel> GetTournament(int id)
		{
			var tournamentModel = await _tournamentSevice.GetTournamentByIdAsync(id);

			return tournamentModel!;
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Tournament>))]
		public async Task<IEnumerable<TournamentModel>> GetTournaments()
		{
			var tournamentModels = await _tournamentSevice.GetTournamentsAsync();

			return tournamentModels;
		}

		[HttpPost("CreateTournament")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateTournament(TournamentModel model)
		{
			var validationResult = await _touranmentValidator.ValidateAsync(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult);

			var result = await _tournamentSevice.AddTournamentAsync(model);

			if (result)
				return Created();

			return StatusCode(500, "Internal Server Error");
		}

		[HttpPut("UpdateTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateTournament(TournamentModel model, long tournamentId)
		{
			var validationResult = await _touranmentValidator.ValidateAsync(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult);

			var result = await _tournamentSevice.UpdateTournamentAsync(model, tournamentId);

			if (result)
				return Ok();

			return StatusCode(500, "Internal Server Error");
		}

		[HttpDelete("DeleteTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteTournament(int id)
		{
			var result = await _tournamentSevice.DeleteTournamentAsync(id);

			if (result)
				return Ok();

			return NoContent();
		}

	}
}
