using Tournaments.Domain.Statuses;

namespace Tournaments.Domain.Entities
{
	public class Team
	{
        public long Id { get; set; }
        public long OwnerId { get; set; }

        public List<AppUser> Players { get; set; } = null!;

        public List<TournamentTeam> TournamentTeams { get; set; } = null!;

		public string TeamName { get; set; } = null!;
        public TournamentTeamStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
