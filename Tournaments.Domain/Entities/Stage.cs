namespace Tournaments.Domain.Entities
{
	public class Stage
	{
        public long Id { get; set; }
        public int StageNumber { get; set; }
        public IList<Match> Matches { get; set; } = null!;
    }
}
