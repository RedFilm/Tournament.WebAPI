namespace Tournaments.Domain.Models.TeamJoinRequestModels
{
	public class TeamJoinRequestModel
	{
		public long SenderId { get; set; }
		public long RecipientId { get; set; }
		public long TeamId { get; set; }
		public string Message { get; set; } = null!;
	}
}
