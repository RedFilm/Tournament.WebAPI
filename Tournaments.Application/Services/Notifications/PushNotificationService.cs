using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Hubs;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Enums.Statuses;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Interfaces.Services;
using Tournaments.Domain.Models;

namespace Tournaments.Application.Services.Notifications
{
	public class PushNotificationService : INotificationService
	{
		private readonly IHubContext<NotificationHub> _notificationHubContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly INotificationRepository _notificationRepository;
		private readonly IMapper _mapper;

		public PushNotificationService(IHubContext<NotificationHub> notificationHubContext,
			INotificationRepository notificationRepository,
			UserManager<AppUser> userManager,
			IMapper mapper)
		{
			_notificationHubContext = notificationHubContext;
			_notificationRepository = notificationRepository;
			_userManager = userManager;
			_mapper = mapper;
		}

		// TODO: Пока не знаю, как отсылать уведомления группе пользователей. Например, системные уведомления для всех подписавшихся.
		public async Task SendAsync(NotificationModel notificationModel)
		{
			if (!await _userManager.Users.AnyAsync(u => u.Id == notificationModel.ReceiverId))
				throw new NotFoundException("Receiver not found");

			await _notificationHubContext.Clients.Groups($"user-{notificationModel.ReceiverId}").SendAsync("ReceiveNotification", notificationModel);
		}

		public async Task<NotificationModel> CreateRequestNotificationAsync(TeamJoinRequest joinRequest)
		{
			var notification = new Notification
			{
				Link = $"api/TeamJoinRequests/{joinRequest.Id}",
				Message = joinRequest.Message,
			};

			await _notificationRepository.AddAsync(notification, joinRequest.ReceiverId);
			return _mapper.Map<NotificationModel>(notification);
		}

		public async Task<NotificationModel> CreateStatusNotificationAsync(TeamJoinRequest joinRequest, TeamJoinRequestStatus status)
		{
			var notification = new Notification
			{
				Link = $"api/TeamJoinRequests/{joinRequest.Id}",
				Message = GetNotificationMessage(TeamJoinRequestStatus.Accepted)
			};

			await _notificationRepository.AddAsync(notification, joinRequest.SenderId);
			return _mapper.Map<NotificationModel>(notification);
		}

		private string GetNotificationMessage(TeamJoinRequestStatus status)
		{
			switch (status)
			{
				case TeamJoinRequestStatus.Accepted:
					return "Your join request has been accepted!";
				case TeamJoinRequestStatus.Rejected:
					return "Your join request has been rejected.";
				default:
					return "Your join request status has been updated.";
			}
		}
	}
}
