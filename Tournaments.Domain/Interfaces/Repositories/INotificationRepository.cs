using Tournaments.Domain.Entities;
using Tournaments.Domain.Models;

namespace Tournaments.Domain.Interfaces.Repositories
{
	public interface INotificationRepository
	{
		/// <summary>
		/// Asynchronously gets all user notifications.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <param name="userId">User id</param>
		/// <returns>List of all user notifications</returns>
		Task<List<Notification>> GetAllAsync(PaginationModel model, long userId);

		/// <summary>
		/// Asynchronously gets unread user notifications.
		/// </summary>
		/// <param name="model">Model that contains page number and size</param>
		/// <param name="userId">User id</param>
		/// <returns>List of unread user notifications</returns>
		Task<List<Notification>> GetUnreadAsync(PaginationModel model, long userId);
	}
}
