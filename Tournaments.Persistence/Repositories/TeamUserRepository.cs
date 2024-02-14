using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	public class TeamUserRepository : ITeamUserRepository
	{
		private readonly TournamentDbContext _context;

		public TeamUserRepository(TournamentDbContext context)
        {
            _context = context;
        }

		/// <inheritdoc />
		public async Task<bool> AnyAsync(long teamId, long userId)
		{
			var team = await _context.Teams.Include(t => t.Players)
				.FirstOrDefaultAsync(t => t.Id == teamId);

			return team!.Players.FirstOrDefault(t => t.Id == userId) is not null;
		}
	}
}
