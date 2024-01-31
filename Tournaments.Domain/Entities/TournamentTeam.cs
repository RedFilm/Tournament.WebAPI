using Tournaments.Domain.Statuses;

namespace Tournaments.Domain.Entities
{
	public class TournamentTeam
	{
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

		public int TournamentId { get; set; }
		public Tournament Tournament { get; set; } = null!;

		public TournamentTeamStatus Status { get; set; }
    }
}
