using Microsoft.EntityFrameworkCore;
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
		public async Task<bool> AddTeamAsync(Team team)
		{
			await _context.Teams.AddAsync(team);

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
		public async Task<Team?> GetTeamAsync(long id)
		{
			return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id) ?? null;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Team>> GetTeamsAsync(long tournamentId)
		{
			var tournamentTeams = from tournamentTeam in _context.TournamentTeams
								  join team in _context.Teams on tournamentTeam.TeamId equals team.Id
								  where tournamentTeam.TournamentId == tournamentId
								  select team;

			return await tournamentTeams.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateTeamAsync(Team team)
		{
			_context.Teams.Update(team);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
