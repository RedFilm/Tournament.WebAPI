namespace Tournaments.Domain.Models
{
	public class StageModel
	{
		public long Id { get; set; }
		public int StageNumber { get; set; }
		public List<MatchModel> Matches { get; set; } = null!;
	}
}
