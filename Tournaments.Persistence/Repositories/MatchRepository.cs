using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	internal class MatchRepository : IMatchRepository
	{
		private readonly TournamentDbContext _tournamentDbContext;

		public MatchRepository(TournamentDbContext tournamentDbContext)
        {
			_tournamentDbContext = tournamentDbContext;
		}

        public async Task<Match?> GetMatchByIdAsync(long matchId)
		{
			return await _tournamentDbContext.Matches.FirstOrDefaultAsync(m => m.MatchId == matchId);
		}

		public async Task<List<Match>> GetMatchesAsync(long stageId)
		{
			return await _tournamentDbContext.Matches.Where(m => m.StageId == stageId).ToListAsync();
		}
	}
}
