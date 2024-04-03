using Tournaments.Domain.Enums.Statuses;

namespace Tournaments.Domain.Entities
{
    public class TournamentTeam
	{
        public long TeamId { get; set; }
		public long PlayerId { get; set; }
		public long TournamentId { get; set; }
		
		public Team Team { get; set; } = null!;
        public AppUser Player { get; set; } = null!;
		public Tournament Tournament { get; set; } = null!;

		public TournamentTeamStatus Status { get; set; }
    }
}
