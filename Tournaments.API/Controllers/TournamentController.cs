using Microsoft.AspNetCore.Mvc;
using System;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private readonly ITournamentRepository _tournamentRepository;

		public TournamentController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

		[HttpGet("GetTournament/{id}")]
		public async Task<IActionResult> GetTournament(int id)
		{
			var result = await _tournamentRepository.GetTournamentAsync(id);

			if (result is not null)
				return Ok(result);
			return BadRequest();
		}

		[HttpGet("GetTournaments")]
		public async Task<IActionResult> GetTournaments()
		{
			var result = await _tournamentRepository.GetTournamentsAsync();

			return Ok(result);
		}

		[HttpPost("CreateTournament")]
		public async Task<IActionResult> CreateTournament(TournamentModel model)
		{
			var result = await _tournamentRepository.AddTournamentAsync(model);

			if(result)
				return Ok();
			return BadRequest("Something went wrong");
		}

		[HttpPut("UpdateTournament")]
		public async Task<IActionResult> UpdateTournament(TournamentModel model)
		{
			var result = await _tournamentRepository.UpdateTournamentAsync(model);

			if (result)
				return Ok();
			return BadRequest("Something went wrong");
		}

		[HttpDelete("DeleteTournament/{id}")]
		public async Task<IActionResult> DeleteTournament(int id)
		{
			var result = await _tournamentRepository.DeleteTournamentAsync(id);

			if (result)
				return Ok();
			return BadRequest("Something went wrong");
		}
	}
}
