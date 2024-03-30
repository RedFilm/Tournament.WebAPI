namespace Tournaments.Domain.Entities
{
	public class Stage
	{
        public long Id { get; set; }
        public long BracketId { get; set; }
		public Bracket Bracket { get; set; } = null!;
		public int StageNumber { get; set; }
        public IList<Match> Matches { get; set; } = null!;
    }
}
