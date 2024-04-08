using Tournaments.Domain.Interfaces.Repositories;

namespace Tournaments.Persistence.Repositories
{
	public class TeamJoinRequestRepository : ITeamJoinRequestRepository
	{
		public Task<bool> AddJoinRequestAsync(TeamJoinRequestModel model)
		{
			throw new NotImplementedException();
		}
	}
}
