using Microsoft.AspNetCore.SignalR;
using Tournaments.Application.Hubs;
using Tournaments.Domain.Interfaces.Services;

namespace Tournaments.Application.Services.Notifications
{
	public class PushNotificationService : INotificationService
	{
		private readonly IHubContext<NotificationHub> _notificationHubContext;

		public PushNotificationService(IHubContext<NotificationHub> notificationHubContext)
		{
			_notificationHubContext = notificationHubContext;
		}

		public async Task SendNotificationAsync(long toUserId, string message)
		{
			// Connection checking
			await _notificationHubContext.Clients.User(toUserId.ToString()).SendAsync("ReceiveNotification", message);
		}
	}
}
