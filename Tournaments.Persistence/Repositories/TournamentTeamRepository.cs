using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	public class TournamentTeamRepository : ITournamentTeamRepository
	{
		private readonly TournamentDbContext _context;

		public TournamentTeamRepository(TournamentDbContext context)
		{
			_context = context;
		}

		public async Task<List<long>> GetTournamentTeamPlayerIdsAsync(long tournamentId, long teamId)
		{
			return await _context.TournamentTeams
				.Where(tt => tt.TournamentId == tournamentId && tt.TeamId == teamId)
				.Select(tt => tt.PlayerId)
				.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<bool> AnyAsync(long tournamentId, long teamId, IEnumerable<long> playerIds)
		{
			return await _context.TournamentTeams.AnyAsync(
				tt => tt.TournamentId == tournamentId &&
				tt.TeamId == teamId &&
				playerIds.Any(id => id == tt.PlayerId));
		}
	}
}
