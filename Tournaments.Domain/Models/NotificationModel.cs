namespace Tournaments.Domain.Models
{
	public class NotificationModel
	{
		public long Id { get; set; }
		public string Message { get; set; } = null!;
		public string Link { get; set; } = null!;
		public bool IsRead { get; set; } = false;
	}
}
