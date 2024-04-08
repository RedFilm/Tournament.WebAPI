using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	public class TeamJoinRequestRepository : ITeamJoinRequestRepository
	{
		private readonly TournamentDbContext _context;

		public TeamJoinRequestRepository(TournamentDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddJoinRequestAsync(TeamJoinRequest joinRequest)
		{
			await _context.TeamJoinRequests.AddAsync(joinRequest);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
