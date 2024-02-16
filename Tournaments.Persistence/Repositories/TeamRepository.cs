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
			team.Players = team.Players ?? new List<AppUser>();
			team.Players.Add(player);
			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> RemovePlayerFromTeamAsync(AppUser player, long teamId)
		{
			var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == teamId);

			team?.Players.Remove(player);
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
			return await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Tournament>> GetTournamentsAsync(long teamId)
		{
			var teamTournaments = _context.TournamentTeams
				.Where(tournamentTeam => tournamentTeam.TeamId == teamId)
				.Include(tournamentTeam => tournamentTeam.Tournament)
				.Select(tournamentTeam => tournamentTeam.Tournament);

			return await teamTournaments.ToListAsync();
		}
		public async Task<IEnumerable<AppUser>> GetTeamPlayersAsync(long teamId)
		{
			var team = await _context.Teams.Include(t => t.Players)
				.AsNoTracking()
				.FirstOrDefaultAsync(t => t.Id == teamId);

			return team!.Players;
		}

		public async Task<IEnumerable<Team>> GetTeamsAsync()
		{
			return await _context.Teams.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<bool> UpdateTeamAsync(Team team)
		{
			_context.Teams.Update(team);
			return await _context.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> AnyAsync(long teamId)
		{
			return await _context.Teams.AsNoTracking().FirstOrDefaultAsync(t => t.Id == teamId) is not null;
		}
	}
}
