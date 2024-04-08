using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models.TeamJoinRequestModels;

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
