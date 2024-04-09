using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Hubs;
using Tournaments.Application.Hubs.Interfaces;
using Tournaments.Application.Hubs.Managers;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.Application.Services.Notifications
{
	public class PushNotificationService : INotificationService
	{
		private readonly IHubContext<NotificationHub> _notificationHubContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConnectionManager _connectionManager;

		public PushNotificationService(IHubContext<NotificationHub> notificationHubContext,
			UserManager<AppUser> userManager,
			IConnectionManager connectionManager)
		{
			_notificationHubContext = notificationHubContext;
			_userManager = userManager;
			_connectionManager = connectionManager;
		}

		public async Task SendNotificationAsync(long toUserId, string message)
		{
			if (!await _userManager.Users.AnyAsync(u => u.Id == toUserId))
				throw new NotFoundException("User not found");

			if (!_connectionManager.IsUserConnected(toUserId))
			{
				// Если в данный момент получатель офлайн
				// ..
				// ..
			}

			await _notificationHubContext.Clients.User(toUserId.ToString()).SendAsync("ReceiveNotification", message);
		}
	}
}
