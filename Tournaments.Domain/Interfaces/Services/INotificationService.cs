using Tournaments.Domain.Entities;
using Tournaments.Domain.Enums.Statuses;
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

		public Task<NotificationModel> CreateRequestNotificationAsync(TeamJoinRequest joinRequest);
		public Task<NotificationModel> CreateStatusNotificationAsync(TeamJoinRequest joinRequest, TeamJoinRequestStatus status);
	}
}
