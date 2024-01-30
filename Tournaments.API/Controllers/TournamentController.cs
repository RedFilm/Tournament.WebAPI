using Microsoft.AspNetCore.Mvc;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;
using Tournaments.Domain.ViewModels;

namespace Tournaments.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentController : ControllerBase
	{
		private ITournamentRepository _tournamentRepository;

		public TournamentController(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

		[HttpGet("GetTournament/{id}")]
		public async Task<IActionResult> GetTournament(int id)
		{
			var result = _tournamentRepository.GetTournamentAsync(id);

			if(result != null)
				return Ok(result);
			return BadRequest();
		}

		[HttpGet("GetTournaments")]
		public async Task<IActionResult> GetTournaments()
		{
			var result = await _tournamentRepository.GetTournamentsAsync();

			if (result != null)
				return Ok(result);
			return BadRequest();
		}

		[HttpPost("CreateTournament")]
		public async Task<IActionResult> CreateTournament(TournamentViewModel tournament)
		{
			if(!ModelState.IsValid)
				return BadRequest();

			var result = await _tournamentRepository.AddTournamentAsync(tournament);

			if(result)
				return Ok();
			return BadRequest("Something went wrong");
		}

		[HttpPut("UpdateTournament")]
		public async Task<IActionResult> UpdateTournament(TournamentViewModel vm)
		{
			var result = await _tournamentRepository.UpdateTournamentAsync(vm);

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
