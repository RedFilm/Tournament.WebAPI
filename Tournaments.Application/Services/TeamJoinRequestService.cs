using AutoMapper;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Enums.Statuses;
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
		private readonly ITeamService _teamService;

		public TeamJoinRequestService(ITeamJoinRequestRepository teamJoinRequestRepository,
			INotificationService pushNotificationService,
			ITeamService teamService,
			IMapper mapper)
		{
			_teamJoinRequestRepository = teamJoinRequestRepository;
			_pushNotificationService = pushNotificationService;
			_teamService = teamService;
			_mapper = mapper;
		}

		public async Task CreateJoinRequestAsync(TeamJoinRequestModel model)
		{
			var joinRequest = _mapper.Map<TeamJoinRequest>(model);
			joinRequest.Status = TeamJoinRequestStatus.Pending;
			joinRequest.Created = DateTimeOffset.UtcNow;
			var isSuccessful = await _teamJoinRequestRepository.AddJoinRequestAsync(joinRequest);

			if (!isSuccessful)
				throw new Exception("Couldn't save to the database");

			await _pushNotificationService.SendNotificationAsync(model.ReceiverId, model.Message);
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

		public async Task AcceptRequestAsync(long requestId, TeamJoinRequestModel model)
		{
			var teamJoinRequest = await _teamJoinRequestRepository.GetRequestByIdAsync(requestId);

			if (teamJoinRequest is null)
				throw new NotFoundException("Request doesn't exist.");

			if (!await _teamService.AddPlayerToTeamAsync(model.TeamId, model.SenderId, model.ReceiverId))
				throw new Exception("Couldn't save to the database");

			teamJoinRequest.Status = TeamJoinRequestStatus.Accepted;

			await _teamJoinRequestRepository.UpdateRequestAsync(teamJoinRequest);

			await _pushNotificationService.SendNotificationAsync(model.SenderId, model.Message);
		}

		public async Task RejectRequestAsync(long requestId, TeamJoinRequestModel model)
		{
			var teamJoinRequest = await _teamJoinRequestRepository.GetRequestByIdAsync(requestId);

			if (teamJoinRequest is null)
				throw new NotFoundException("Request doesn't exist.");

			teamJoinRequest.Status = TeamJoinRequestStatus.Rejected;

			await _teamJoinRequestRepository.UpdateRequestAsync(teamJoinRequest);

			await _pushNotificationService.SendNotificationAsync(model.SenderId, model.Message);
		}
	}
}
