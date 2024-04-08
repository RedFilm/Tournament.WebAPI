using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
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
		private readonly IMapper _mapper;

		public TeamJoinRequestService(ITeamJoinRequestRepository teamJoinRequestRepository,
			INotificationService pushNotificationService,
			IMapper mapper)
		{
			_teamJoinRequestRepository = teamJoinRequestRepository;
			_pushNotificationService = pushNotificationService;
			_mapper = mapper;
		}

		public async Task CreateJoinRequestAsync(TeamJoinRequestModel model)
		{
			var joinRequest = _mapper.Map<TeamJoinRequest>(model);
			var isSuccesfull = await _teamJoinRequestRepository.AddJoinRequestAsync(joinRequest);

			if (!isSuccesfull)
				throw new Exception("Couldn't save to the database");

			await _pushNotificationService.SendNotificationAsync(model.RecipientId, model.Message);
		}

		public async Task<TeamJoinRequestWithIdModel> GetRequestByIdAsync(long requestId)
		{
			var joinRequest = await _teamJoinRequestRepository.GetRequestByIdAsync(requestId);

			if (joinRequest is null)
				throw new NotFoundException("Join request doesn't exist");

			return _mapper.Map<TeamJoinRequestWithIdModel>(joinRequest);
		}

		public async Task<PageModel<TeamJoinRequestWithIdModel>> GetAllAsync(PaginationModel model)
		{
			var joinRequests = await _teamJoinRequestRepository.GetAllAsync(model);
			var joinRequestsModels = _mapper.Map<IEnumerable<TeamJoinRequestWithIdModel>>(joinRequests);

			return new PageModel<TeamJoinRequestWithIdModel>
			{
				PageNumber = model.PageNumber,
				PageSize = model.PageSize,
				Items = joinRequestsModels,
				ItemsCount = await _teamJoinRequestRepository.GetTotalCountAsync()
			};
		}
	}
}
