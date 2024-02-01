using Tournaments.Domain.Statuses;

namespace Tournaments.Domain.Entities
{
	public class TournamentTeam
	{
        public long TeamId { get; set; }
        public Team Team { get; set; } = null!;

		public long TournamentId { get; set; }
		public Tournament Tournament { get; set; } = null!;

		public TournamentTeamStatus Status { get; set; }
    }
}
