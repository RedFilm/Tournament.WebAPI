using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
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

		public async Task CreateJoinRequestAsync(TeamJoinRequestModel model)
		{
			throw new NotImplementedException();
		}

		public async Task<TeamJoinRequestModel> GetRequestByIdAsync(long requestId)
		{
			throw new NotImplementedException();
		}

		public async Task<PageModel<TeamJoinRequestWithIdModel>> GetAllAsync(PaginationModel model)
		{
			throw new NotImplementedException();
		}
	}
}
