using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;
using Tournaments.Domain.Validators;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentRepository _tournamentRepository;
		private readonly IValidator<TournamentModel> _touranmentValidator;

		public TournamentController(ITournamentRepository tournamentRepository, IValidator<TournamentModel> touranmentValidator)
        {
            _tournamentRepository = tournamentRepository;
			_touranmentValidator = touranmentValidator;

		}

		[HttpGet("GetTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tournament))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetTournament(int id)
		{
			var result = await _tournamentRepository.GetTournamentAsync(id);

			if (result is not null)
				return Ok(result);

			return NotFound();
		}

		[HttpGet("GetTournaments")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Tournament>))]
		public async Task<IActionResult> GetTournaments()
		{
			var result = await _tournamentRepository.GetTournamentsAsync();

			return Ok(result);
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

			var result = await _tournamentRepository.AddTournamentAsync(model);

			if (result)
				return Created();

			return StatusCode(500, "Internal Server Error");
		}

		[HttpPut("UpdateTournament")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> UpdateTournament(TournamentModel model)
		{
			var validationResult = await _touranmentValidator.ValidateAsync(model);

			if (!validationResult.IsValid)
				return BadRequest(validationResult);

			var result = await _tournamentRepository.UpdateTournamentAsync(model);

			if (result)
				return Ok();

			return StatusCode(500, "Internal Server Error");
		}

		[HttpDelete("DeleteTournament/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteTournament(int id)
		{
			var result = await _tournamentRepository.DeleteTournamentAsync(id);

			if (result)
				return Ok();

			return NoContent();
		}

	}
}
