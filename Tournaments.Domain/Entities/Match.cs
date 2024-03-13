namespace Tournaments.Domain.Entities
{
	public class Match
	{
		public long MatchId { get; set; }
		public int Identifier { get; set; }

		public long? WinnerId { get; set; }
		public long? Team1Id { get; set; }
		public long? Team2Id { get; set; }

		public Team? Team1 { get; set; }
		public Team? Team2 { get; set; }
		public Team? Winner { get; set; }

		public Stage Stage { get; set; } = null!;
	}
}
