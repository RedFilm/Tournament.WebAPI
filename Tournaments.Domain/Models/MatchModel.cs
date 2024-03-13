using Tournaments.Domain.Entities;

namespace Tournaments.Domain.Models
{
	public class MatchModel
	{
		public long MatchId { get; set; }
		public int Identifier { get; set; }

		public long? WinnerId { get; set; }
		public long? Team1Id { get; set; }
		public long? Team2Id { get; set; }

		public long StageId { get; set; }
	}
}
