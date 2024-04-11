using Tournaments.Domain.Enums.Statuses;

namespace Tournaments.Domain.Entities
{
	public class TeamJoinRequest
	{
		public long Id { get; set; }
		public long SenderId { get; set; }
		public long ReceiverId { get; set; }
		public long TeamId { get; set; }
		public string Message { get; set; } = null!;

		public AppUser Sender { get; set; } = null!;
		public AppUser Receiver { get; set; } = null!;
		public Team Team { get; set; } = null!;

		public DateTimeOffset Created { get; set; }
		public TeamJoinRequestStatus Status { get; set; }
	}
}
