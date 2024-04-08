using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.Application.Services
{
	public class TeamJoinRequestService : ITeamJoinRequestService
	{
		private readonly ITeamJoinRequestRepository _teamJoinRequestRepository;

		public TeamJoinRequestService(ITeamJoinRequestRepository teamJoinRequestRepository)
		{
			_teamJoinRequestRepository = teamJoinRequestRepository;
		}

		public async Task SendJoinRequestAsync(TeamJoinRequestModel model)
		{

		}
	}
}
