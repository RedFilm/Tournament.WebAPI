namespace Tournaments.Domain.Models
{
	public class MatchResultModel
	{
        public int StageNumber { get; set; }
        public long MatchId { get; set; }
        public long WinnerId { get; set; }
    }
}
