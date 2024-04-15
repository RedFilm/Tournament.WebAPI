using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Services
{
	public interface INotificationService
	{
		/// <summary>
		/// Send notification to specified user.
		/// </summary>
		///<param name="notificationModel">Model that contains notification id, receiver, link, message and flag indicating whether it has been read</param>
		/// <returns></returns>
		Task SendAsync(NotificationModel notificationModel);
	}
}
