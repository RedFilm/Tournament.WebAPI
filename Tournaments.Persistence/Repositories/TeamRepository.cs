using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	public class TeamRepository : ITeamRepository
	{
		private readonly TournamentDbContext _context;

		public TeamRepository(TournamentDbContext contect)
        {
            _context = contect;
        }

		/// <inheritdoc />
		public async Task<bool> AddPlayerToTeamAsync(AppUser player, Team team)
		{
			team.Players.Add(player);
			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> RemovePlayerFromTeamAsync(AppUser player, Team team)
		{
			team.Players.Remove(player);
			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> AddTeamAsync(Team team)
		{
			await _context.Teams.AddAsync(team);

			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> AddTeamToTournamentAsync(Tournament tournament, Team team)
		{
			_context.TournamentTeams.Add(new TournamentTeam { Tournament = tournament, Team = team });

			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteTeamAsync(long id)
		{
			var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

			if (team is null)
				return false;

			_context.Teams.Remove(team);

			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<Team?> GetTeamByIdAsync(long id)
		{
			return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id) ?? null;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Tournament?>> GetTournamentsAsync(long teamId)
		{
			var teamTournaments = from tournamentTeam in _context.TournamentTeams
								  join tournament in _context.Tournaments on tournamentTeam.TeamId equals tournament.Id
								  where tournamentTeam.TournamentId == teamId
								  select tournament;

			return await teamTournaments.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateTeamAsync(Team team)
		{
			_context.Teams.Update(team);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
