namespace Tournaments.Domain.Models.TeamJoinRequestModels
{
	public class TeamJoinRequestWithIdModel : TeamJoinRequestModel
	{
		public long Id { get; set; }
		public DateTimeOffset Created { get; set; }
	}
}
