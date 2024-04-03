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

		/// <inheritdoc />
		public async Task<bool> AnyAsync(long tournamentId, long teamId, IEnumerable<long> playerIds)
		{
			return await _context.TournamentTeams.AnyAsync(
				tt => tt.TournamentId == tournamentId &&
				tt.TeamId == teamId &&
				playerIds.Any(pid => pid == tt.PlayerId));
		}
	}
}
