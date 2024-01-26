using Tournaments.Domain.Statuses;

namespace Tournaments.Domain.Models
{
	public class TournamentTeam
	{
        public int TeamId { get; set; }
        public Team Team { get; set; }

		public int TournamentId { get; set; }
		public Tournament Tournament { get; set; }

		public TournamentTeamStatus Status { get; set; }
    }
}
