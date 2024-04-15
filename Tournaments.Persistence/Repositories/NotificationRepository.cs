using Microsoft.EntityFrameworkCore;
using Tournaments.Domain.Entities;
using Tournaments.Domain.Interfaces.Repositories;
using Tournaments.Domain.Models;

namespace Tournaments.Persistence.Repositories
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly TournamentDbContext _context;

		public NotificationRepository(TournamentDbContext context)
		{
			_context = context;
		}

		public async Task<List<Notification>> GetAllAsync(PaginationModel model, long userId)
		{
			var skipCount = (model.PageNumber - 1) * model.PageSize;

			var notifications = await _context.Notifications
				.Include(n => n.UserNotifications)
				.Where(n => n.UserNotifications.Any(un => un.UserId == userId))
				.Skip(skipCount)
				.Take(model.PageSize)
				.ToListAsync();

			return notifications;
		}

		public async Task<List<Notification>> GetUnreadAsync(PaginationModel model, long userId)
		{
			var skipCount = (model.PageNumber - 1) * model.PageSize;

			var unreadNotifications = await _context.UserNotifications
				.Where(un => un.UserId == userId && !un.IsRead)
				.Select(un => un.Notification)
				.Skip(skipCount)
				.Take(model.PageSize)
				.ToListAsync();

			return unreadNotifications;
		}

		public async Task<int> GetCountForUserAsync(long userId, bool isReadOnly)
		{
			if (isReadOnly)
				return await _context.UserNotifications.CountAsync(un => un.UserId == userId && un.IsRead);

			return await _context.UserNotifications.CountAsync(un => un.UserId == userId);
		}
	}
}
