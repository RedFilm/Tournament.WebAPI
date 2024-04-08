using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;
using Tournaments.Domain.Models.TeamJoinRequestModels;

namespace Tournaments.Application.Services
{
	public class TeamJoinRequestService : ITeamJoinRequestService
	{
		private readonly ITeamJoinRequestRepository _teamJoinRequestRepository;
		private readonly INotificationService _pushNotificationService;

		public TeamJoinRequestService(ITeamJoinRequestRepository teamJoinRequestRepository,
			INotificationService pushNotificationService)
		{
			_teamJoinRequestRepository = teamJoinRequestRepository;
			_pushNotificationService = pushNotificationService;
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
