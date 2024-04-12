namespace Tournaments.Domain.Entities
{
	public class UserNotification
	{
		public long UserId { get; set; }
		public long NotificationId { get; set; }
		public bool IsRead { get; set; }

		public AppUser User { get; set; } = null!;
		public Notification Notification { get; set; } = null!;
	}
}
