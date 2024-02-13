using Microsoft.AspNetCore.Identity;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Persistence.Repositories;

namespace Tournaments.Application.Services
{
	public class TeamService : ITeamService
	{
		private readonly ITeamRepository _teamRepository;
		private readonly UserManager<AppUser> _userManager;

		public TeamService(ITeamRepository teamRepository, UserManager<AppUser> userManager)
        {
			_teamRepository = teamRepository;
			_userManager = userManager;
		}

        public async Task<bool> AddPlayerToTeamAsync(long teamId, long playerId)
		{
			var team = await _teamRepository.GetTeamAsync(teamId);
			if (await _teamRepository.GetTeamAsync(teamId) is null)
				throw new NotFoundException("Team doesn't exist");

			var user = await _userManager.FindByIdAsync(playerId.ToString());
			if (user is null)
				throw new NotFoundException("User doesn't exist");

			var teamPlayers = await _teamRepository.GetTeamPlayersAsync(teamId);

			if (teamPlayers!.Players.Contains(user))
				//throw new AlreadyExistsException("User already is in team");

			if (await _teamRepository.AddPlayerToTeamAsync(user, team!))
				return true;
			return false;
		}

		public async Task<bool> CreateTeamAsync(Team team)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteTeamAsync(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<Team?> GetTeamByIdAsync(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Team>> GetTeamsAsync(long tournamentId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> RegisterTeamForTournamentAsync(long teamId, long tournamentId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> RemovePlayerFromTeamAsync(long teamId, long playerId)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateTeamAsync(Team team, long id)
		{
			throw new NotImplementedException();
		}
	}
}
