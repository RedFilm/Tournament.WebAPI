using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tournaments.Application.Hubs;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Exceptions;
using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.Application.Services.Notifications
{
	public class PushNotificationService : INotificationService
	{
		private readonly IHubContext<NotificationHub> _notificationHubContext;
		private readonly UserManager<AppUser> _userManager;

		public PushNotificationService(IHubContext<NotificationHub> notificationHubContext,
			UserManager<AppUser> userManager)
		{
			_notificationHubContext = notificationHubContext;
			_userManager = userManager;
		}

		public async Task SendAsync(long receiverId, string message)
		{
			if (!await _userManager.Users.AnyAsync(u => u.Id == receiverId))
				throw new NotFoundException("User not found");

			await _notificationHubContext.Clients.Groups($"user-{receiverId}").SendAsync("ReceiveNotification", message);
		}
	}
}
