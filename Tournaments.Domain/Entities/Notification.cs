namespace Tournaments.Domain.Entities
{
	public class Notification
	{
		public long Id { get; set; }
		public string Message { get; set; } = null!;
		public string Link { get; set; } = null!;

		public List<UserNotification> UserNotifications { get; set; } = null!;
	}
}
