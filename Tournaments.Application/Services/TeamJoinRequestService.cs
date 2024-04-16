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
			if (await _teamJoinRequestRepository.AnyAsync(model.SenderId, model.TeamId))
				throw new AlreadyExistsException("The request has already been sent.");

			var joinRequest = _mapper.Map<TeamJoinRequest>(model);
			joinRequest.Status = TeamJoinRequestStatus.Pending;
			joinRequest.Created = DateTimeOffset.UtcNow;
			var isSuccessful = await _teamJoinRequestRepository.AddJoinRequestAsync(joinRequest);

			if (!isSuccessful)
				throw new Exception("Couldn't save to the database");

			var notificationModel = await _pushNotificationService.CreateRequestNotificationAsync(joinRequest);

			await _pushNotificationService.SendAsync(notificationModel);
		}

		public async Task<TeamJoinRequestWithIdModel> GetRequestByIdAsync(long requestId)
		{
			var joinRequest = await GetExistingRequestAsync(requestId);
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
			var teamJoinRequest = await GetExistingRequestAsync(requestId);

			if (!await _teamService.AddPlayerToTeamAsync(model.TeamId, model.SenderId, model.ReceiverId))
				throw new Exception("Couldn't save to the database");

			teamJoinRequest.Status = TeamJoinRequestStatus.Accepted;
			await _teamJoinRequestRepository.UpdateRequestAsync(teamJoinRequest);

			var notificationModel = await _pushNotificationService.CreateStatusNotificationAsync(teamJoinRequest, TeamJoinRequestStatus.Accepted);

			await _pushNotificationService.SendAsync(notificationModel);
		}

		public async Task RejectRequestAsync(long requestId, TeamJoinRequestModel model)
		{
			var teamJoinRequest = await GetExistingRequestAsync(requestId);

			teamJoinRequest.Status = TeamJoinRequestStatus.Rejected;
			await _teamJoinRequestRepository.UpdateRequestAsync(teamJoinRequest);

			var notificationModel = await _pushNotificationService.CreateStatusNotificationAsync(teamJoinRequest, TeamJoinRequestStatus.Rejected);

			await _pushNotificationService.SendAsync(notificationModel);
		}

		private async Task<TeamJoinRequest> GetExistingRequestAsync(long requestId)
		{
			var teamJoinRequest = await _teamJoinRequestRepository.GetRequestByIdAsync(requestId);

			if (teamJoinRequest is null)
				throw new NotFoundException("Request doesn't exist.");
			return teamJoinRequest;
		}
	}
}
