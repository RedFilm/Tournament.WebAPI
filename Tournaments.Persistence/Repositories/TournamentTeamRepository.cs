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
		public async Task<bool> AnyAsync(long tournamentId, long teamId)
		{
			var tournamentTeam = await _context.TournamentTeams
				.FirstOrDefaultAsync(tt => tt.TournamentId == tournamentId && tt.TeamId == teamId);

			return tournamentTeam is not null;
		}
	}
}
